namespace SpiderDocsModule.Models
{
    public class ShellBehaviour
    {    
		public int id { get;set;} = 0;
        public string extension { get; set; } = string.Empty;
        //public en_Shell default_behaviour { get; set; } = en_Shell.None;
        public en_Shell override_behaviour { get; set; } = en_Shell.None;

        public enum en_Shell
        {
            None = 0,
            Edit ,
            New,
            Open,
            Print,
            Printto
        }

        public ShellBehaviour() { }

	}
}


