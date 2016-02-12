using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G6i
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Loading embedded DLLs from the exe.
            AppDomain.CurrentDomain.AssemblyResolve += (sender, asm) =>
            {
                string dllName = new AssemblyName(asm.Name).Name + ".dll";
                string resource = Array.Find(
                    Assembly.GetExecutingAssembly().GetManifestResourceNames()
                    , res => res.EndsWith(dllName));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);

                    return Assembly.Load(assemblyData);
                }
            };

            Application.Run(new FormMain());
        }
    }
}
