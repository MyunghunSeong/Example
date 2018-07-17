using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace TimerProgram
{
    class RandomImageClass 
    {
        private DirectoryInfo dInfo;
        private List<string> fileNameList;
        
        private string rootFolder;

        public RandomImageClass()
        {
            rootFolder = Environment.CurrentDirectory + "\\resource";
            dInfo = new DirectoryInfo(rootFolder);
            fileNameList = new List<string>();
        }

        public void IMG_GetImageFile()
        {
            foreach (FileInfo file in dInfo.GetFiles())
            {
                if (file.Name.Contains(".gif"))
                    fileNameList.Add(file.Name);
                else
                    continue;
            }

            inner_GetRandomFile();
        }

        private void inner_GetRandomFile()
        {
            int randNum = 0;
            string curDir = string.Empty;

            Random r = new Random((int)DateTime.Now.Ticks);

            randNum = r.Next(0, fileNameList.Count);

            string selectFile = fileNameList[randNum];

            curDir = Environment.CurrentDirectory;

            FileInfo fileInfo = new FileInfo(curDir + "\\resource\\" + selectFile);

            fileInfo.CopyTo(curDir + "\\resource\\image\\image2.gif", true);
        }
    }
}
