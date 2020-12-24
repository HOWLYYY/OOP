using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace NewBackups
{
    class RecoveryPoint
    {
        private Dictionary<string, TypeObject> resources;
        public string name { get; private set; }
        public DateTime date { get; private set; }
        public TypeStorage typeStorage { get; private set; }
        public TypeRestPoint typeRestPoint { get; private set; }

        public RecoveryPoint(Dictionary<string, TypeObject> resources, string preffix, TypeStorage ts, TypeRestPoint trp)
        {
            this.resources = new Dictionary<string, TypeObject>(resources);
            date = DateTime.Now;
            name = String.Format("{0}{1}", preffix, date.ToString("dd_MM_yyyy_HH_mm_ss"));
            typeStorage = ts;
            typeRestPoint = trp;
            if (!Directory.Exists(name))
                Directory.CreateDirectory(name);
        }

        public bool AddFullRP()
        {
            ZipArchive zipArchive = null;
            if (typeStorage == TypeStorage.Arch)
            {
                zipArchive = ZipFile.Open(string.Format("{0}\\backup.zip", name), ZipArchiveMode.Update);
            }

            try
            {
                foreach (var file in resources)
                {
                    var Myfile = Path.GetFileName(file.Key);
                    if (typeStorage == TypeStorage.Arch)
                        zipArchive.CreateEntryFromFile(file.Key, Myfile);
                    else if (typeStorage == TypeStorage.Sep)
                        File.Copy(file.Key, string.Format("{0}\\{1}", name, Myfile));
                }
            }
            catch (Exception e)
            {
                return false;
            }
            if (zipArchive != null)
            {
                zipArchive.Dispose();
            }
            return true;
        }

        public bool AddPartRP(RecoveryPoint recoveryPoint)
        {
            if (recoveryPoint == null)
            {
                return AddFullRP();
            }
            ZipArchive zipArchive = null;
            if (typeStorage == TypeStorage.Arch)
                zipArchive = ZipFile.Open(string.Format("{0}\\backup.zip", name), ZipArchiveMode.Update);

            try
            {
                foreach (var file in resources)
                {
                    if (recoveryPoint.resources.ContainsKey(file.Key))
                    {
                        DateTime dt = File.GetLastWriteTime(file.Key);
                        var Myfile = Path.GetFileName(file.Key);
                        if (dt > recoveryPoint.date)
                        {
                            if (typeStorage == TypeStorage.Arch)
                                zipArchive.CreateEntryFromFile(file.Key, Myfile);
                            if (typeStorage == TypeStorage.Sep)
                                File.Copy(file.Key, string.Format("{0}\\{1}", name, Myfile));
                        }
                    }
                    //искать файл в предыдущей точке, если да, то смотрим его дату обновления, если дата больше чем дата предыдущей точки восстановления, то сохраняем файл(FileInfo)
                }
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }
    }
}