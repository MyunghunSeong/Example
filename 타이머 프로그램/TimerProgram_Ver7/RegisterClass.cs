using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace TimerProgram
{
    class RegisterClass
    {
        private string value = string.Empty;
        private string m_Rpath = @"HKEY_CURRENT_USER\Software\Microsoft\WIndows\CurrentVersion\Run";
        private string m_Rkey = "Timer";
        private string m_Rval = Environment.CurrentDirectory + "\\TimerProgram.exe";
        private RegistryKey m_key;

        public RegistryKey RegKey
        {
            get { return m_key; }
            set { m_key = value; }
        }

        public void RegistreeEnroll()
        {
            /*if ((inner_Registree_Read(m_Rpath, m_Rkey) != m_Rval))
            {
                inner_Registree_Write(m_Rpath, m_Rkey, m_Rval, RegistryValueKind.String);
            }*/
            //inner_RegiStree_Del(m_Rpath, m_Rkey, m_Rval);
        }

        public void RegistreeEnroll(string rKey, string rVal)
        {
            m_key = Registry.CurrentUser.CreateSubKey(rKey);

            if (m_key != null)           
                m_key.SetValue(rKey, rVal);
        }

        private string inner_Registree_Read(string rPath, string rKey)
        {
            RegistryKey reg = null;

            if (rPath.StartsWith("HKEY_CLASSES_ROOT")) reg = Registry.ClassesRoot;
            if (rPath.StartsWith("HKEY_CURRENT_USER")) reg = Registry.CurrentUser;
            if (rPath.StartsWith("HKEY_LOACAL_MACHINE")) reg = Registry.LocalMachine;
            if (rPath.StartsWith("HKEY_USERS")) reg = Registry.Users;
            if (rPath.StartsWith("HKEY_CURRENT_CONFIG")) reg = Registry.CurrentConfig;

            reg = reg.OpenSubKey(rPath.Substring((rPath.IndexOf("\\") + 1),
                rPath.Length - (rPath.IndexOf("\\") + 1)),
                RegistryKeyPermissionCheck.ReadWriteSubTree);

            if (reg == null) return "";
            else return Convert.ToString(reg.GetValue(rKey));
        }

        private void inner_Registree_Write(string rPath, string rKey, string rVal, RegistryValueKind rKnd)
        {
            RegistryKey reg = null;

            if (rPath.StartsWith("HKEY_CLASSES_ROOT")) reg = Registry.ClassesRoot;
            if (rPath.StartsWith("HKEY_CURRENT_USER")) reg = Registry.CurrentUser;
            if (rPath.StartsWith("HKEY_LOACAL_MACHINE")) reg = Registry.LocalMachine;
            if (rPath.StartsWith("HKEY_USERS")) reg = Registry.Users;
            if (rPath.StartsWith("HKEY_CURRENT_CONFIG")) reg = Registry.CurrentConfig;

            reg = reg.CreateSubKey(rPath.Substring((rPath.IndexOf("\\") + 1),
                rPath.Length - (rPath.IndexOf("\\") + 1)),
                RegistryKeyPermissionCheck.ReadWriteSubTree);

            reg.SetValue(rKey, rVal, rKnd);
            reg.Close();
        }

        private void inner_RegiStree_Del(string rPath, string rKey, string rVal)
        {
            RegistryKey reg = null;

            if (rPath.StartsWith("HKEY_CLASSES_ROOT")) reg = Registry.ClassesRoot;
            if (rPath.StartsWith("HKEY_CURRENT_USER")) reg = Registry.CurrentUser;
            if (rPath.StartsWith("HKEY_LOACAL_MACHINE")) reg = Registry.LocalMachine;
            if (rPath.StartsWith("HKEY_USERS")) reg = Registry.Users;
            if (rPath.StartsWith("HKEY_CURRENT_CONFIG")) reg = Registry.CurrentConfig;

            reg = reg.OpenSubKey(rPath.Substring((rPath.IndexOf("\\") + 1),
                rPath.Length - (rPath.IndexOf("\\") + 1)),
                RegistryKeyPermissionCheck.ReadWriteSubTree);

            reg.DeleteValue(rKey);
        }
    }
}
