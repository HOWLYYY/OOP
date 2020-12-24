using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NewBackups
{
    class Backup
    {
        public Dictionary<string, TypeObject> files;
        public Dictionary<DateTime, RecoveryPoint> rest_points;
        public string name { get; private set; }

        public Backup(string name)
        {
            this.name = name;
            files = new Dictionary<string, TypeObject>();
            rest_points = new Dictionary<DateTime, RecoveryPoint>();
        }

        public bool AddResource(string resource)
        {
            TypeObject typeObject;
            if (!files.ContainsKey(resource))
            {
                if (File.Exists(resource))
                    typeObject = TypeObject.Regular;
                else
                    return false;
                files.Add(resource, typeObject);
                return true;
            }
            return false;
        }

        public bool DelResource(string resource)
        {
            TypeObject typeObject;
            if (files.ContainsKey(resource))
            {
                files.Remove(resource, out typeObject);
                return true;
            }
            return false;
        }

        public void PrintResourses()
        {
            foreach (var resource in files)
            {
                Console.WriteLine("\t{0}{1}", resource.Key, Environment.NewLine);
            }
        }

        public void PrintRestPoints()
        {
            foreach (var rp in rest_points)
            {
                Console.WriteLine("\t{0}{1}", rp.Key, Environment.NewLine);
            }
        }

        public bool CreateRestPoint(TypeRestPoint typeRestPoint, TypeStorage typeStorage)
        {
            var rp = new RecoveryPoint(files, string.Format("{0}\\myrp_", name), typeStorage, typeRestPoint);
            var p = rest_points.Keys;
            if ((typeRestPoint == TypeRestPoint.Full && rp.AddFullRP()) ||
                (typeRestPoint == TypeRestPoint.Part && rp.AddPartRP(p.Count > 0 ? rest_points[p.Max()] : null)))
            {
                rest_points.Add(rp.date, rp);
                return true;
            }
            return false;
        }

        public bool RecoveryBackupToRestPoint(string name)
        {
			DateTime dt = Convert.ToDateTime(name);

			if (!rest_points.ContainsKey(dt))
				return false;
            
			return rest_points[dt].Recovery();
        }
    }
}
