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
            date = TrimDate(DateTime.Now, TimeSpan.TicksPerSecond);
            name = String.Format("{0}{1}", preffix, date.ToString("dd_MM_yyyy_HH_mm_ss"));
            typeStorage = ts;
            typeRestPoint = trp;
            if (!Directory.Exists(name))
                Directory.CreateDirectory(name);
        }
        private DateTime TrimDate(DateTime date, long ticks)
        {
            return new DateTime(date.Ticks - (date.Ticks % ticks), date.Kind);
        }

        public bool AddFullRP()
        {
			ZipArchive zipArchive = (typeStorage == TypeStorage.Arch) ?
									ZipFile.Open(string.Format("{0}\\backup.zip", name),
												ZipArchiveMode.Update) :
									null;

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
			ZipArchive zipArchive = (typeStorage == TypeStorage.Arch) ?
						ZipFile.Open(string.Format("{0}\\backup.zip", name),
									ZipArchiveMode.Update) :
						null;
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
                }
            }
            catch(Exception e)
            {
                return false;
            }
            if (zipArchive != null)
            {
                zipArchive.Dispose();
            }
            return true;
        }

		public bool Recovery()
		{
			ZipArchive zipArchive = (typeStorage == TypeStorage.Arch) ?
									ZipFile.Open(string.Format("{0}\\backup.zip", name),
												ZipArchiveMode.Read) :
									null;
			foreach (var file in resources)
			{
				var Myfile = Path.GetFileName(file.Key);
				if (File.Exists(file.Key))
					File.Delete(file.Key);
				if (typeStorage == TypeStorage.Arch)
					 zipArchive.GetEntry(Myfile).ExtractToFile(file.Key);
				else
					File.Copy(string.Format("{0}\\{1}", name, Myfile), file.Key);
			}
            return true;
		}
    }
}