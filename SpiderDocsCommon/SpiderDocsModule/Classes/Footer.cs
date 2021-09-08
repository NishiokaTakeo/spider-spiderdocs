namespace SpiderDocsModule
{
    public enum en_RegAttr
	{
		Id = 0,
		Name,
		Folder,
		DocType,
		Version,
		Author,
		Date,

		Max
	}

	public enum en_FooterType
	{
		RegAttr = 0,
		Attr,

		Max
	}

	public class Footer
	{
		public int id;
		public int field_id;
		public en_FooterType field_type;
	}
}
