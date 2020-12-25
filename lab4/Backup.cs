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
		public int max_size;
		public int max_amount;
		public DateTime max_date;

		public long Size
		{
			get
			{
				return rest_points.Sum(x => x.Value.Size); 
			}
		}

        public Backup(string name)
        {
            this.name = name;
            files = new Dictionary<string, TypeObject>();
            rest_points = new Dictionary<DateTime, RecoveryPoint>();
			max_size = -1;
			max_amount = -1;
			max_date = default;
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

		public void Clean()
		{
			long delta_size = Size - max_size;
			long delta_amount = rest_points.Count - max_amount;
			if (max_amount > 0 && delta_amount > 0)
				CleanAmount(delta_amount);
			else if (max_size > 0 && delta_size > 0)
				CleanSize(delta_size);
			else if (max_date != default(DateTime))
				CleanDate();
		}

		private void CleanAmount(long delta_amount)
		{
			long i = 0;
			foreach (var point in rest_points)
			{
				if (i++ >= max_amount)
					return ;
				point.Value.Clean();
				rest_points.Remove(point.Key);
			}
		}

		private void CleanSize(long delta_size)
		{
			foreach (var point in rest_points)
			{
				if (delta_size <= 0)
					return ;
				point.Value.Clean();
				rest_points.Remove(point.Key);
				delta_size -= point.Value.Size;
			}
		}

		private void CleanDate()
		{
			foreach (var point in rest_points)
			{
				if (point.Key < max_date)
				{
					point.Value.Clean();
					rest_points.Remove(point.Key);
				}
			}
		}
    }
}
