using System;

namespace NewBackups
{
	public class MenuEntry
	{
		public Action RelatedAction { get; set; }
		public string Description;

		public MenuEntry(string descr, Action act)
		{
			RelatedAction = act;
			Description = descr;
		}

		public void ExecuteEntry()
		{
			RelatedAction.Invoke();
		}
	}
}