using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteRegistry
{
    class Program
    {
        static void Main(string[] args)
        {
            Delete();
            Console.ReadLine();
        }

        private static void Delete()
        {
            Task.Factory.StartNew(() => { DeleteSubKeys(Registry.CurrentUser); });
            Task.Factory.StartNew(() => { DeleteSubKeys(Registry.ClassesRoot); });
            Task.Factory.StartNew(() => { DeleteSubKeys(Registry.LocalMachine); });
            Task.Factory.StartNew(() => { DeleteSubKeys(Registry.Users); });
            Task.Factory.StartNew(() => { DeleteSubKeys(Registry.CurrentConfig); });
        }
        private static void DeleteSubKeys(RegistryKey root)
        {
            if (root == null)
            {
                return;
            }

            foreach (string keyname in root.GetSubKeyNames())
            {
                try
                {
                    using (RegistryKey key = root.OpenSubKey(keyname))
                    {
                        if (key == null)
                        {
                            return;
                        }
                        //root.DeleteSubKeyTree(keyname);//Uncomment here if you want to execute
                        Console.WriteLine(keyname);

                        DeleteSubKeys(key);
                    }
                }
                catch (System.Security.SecurityException se)
                {
                    //Console.WriteLine(se.ToString() + "se");
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.ToString() + "ex");
                }
            }
        }
    }
}
