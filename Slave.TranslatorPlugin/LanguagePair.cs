using System.Collections.Generic;

namespace Slave.TranslatorPlugin
{
	public class LanguagePair
	{
	    public string Code { get; set; }

	    public string Caption { get; set; }


	    public LanguagePair(string code, string caption)
		{
			Code = code;
			Caption = caption;
		}

		public static List<LanguagePair> GoogleLanguagePairs
		{
			get
			{
			    var lps = new List<LanguagePair> {
			        new LanguagePair("zh|en", "Du chinois simpl. vers l'anglais"),
			        new LanguagePair("zt|en", "Du chinois trad. vers l'anglais"),
			        new LanguagePair("en|zh", "De l'anglais vers le chinois simpl."),
			        new LanguagePair("en|zt", "De l'anglais vers le chinois trad."),
			        new LanguagePair("en|nl", "De l'anglais vers le hollandais"),
			        new LanguagePair("en|fr", "De l'anglais vers le français"),
			        new LanguagePair("en|de", "De l'anglais vers l'allemand"),
			        new LanguagePair("en|el", "De l'anglais vers le grec"),
			        new LanguagePair("en|it", "De l'anglais vers l'italien"),
			        new LanguagePair("en|ja", "De l'anglais vers le japonais"),
			        new LanguagePair("en|ko", "De l'anglais vers corèen"),
			        new LanguagePair("en|pt", "De l'anglais vers le portugais"),
			        new LanguagePair("en|ru", "De l'anglais vers le russe"),
			        new LanguagePair("en|es", "De l'anglais vers l'espagnol"),
			        new LanguagePair("nl|en", "Du hollandais vers l'anglais"),
			        new LanguagePair("nl|fr", "Du hollandais vers le français"),
			        new LanguagePair("fr|en", "Du français vers l'anglais"),
			        new LanguagePair("fr|de", "Du français vers l'allemand"),
			        new LanguagePair("fr|el", "Du français vers le grec"),
			        new LanguagePair("fr|it", "Du français vers l'italien"),
			        new LanguagePair("fr|pt", "Du français vers le portugais"),
			        new LanguagePair("fr|nl", "Du français vers le hollandais"),
			        new LanguagePair("fr|es", "Du français vers l'espagnol"),
			        new LanguagePair("de|en", "De l'allemand vers l'anglais"),
			        new LanguagePair("de|fr", "De l'allemand vers le français"),
			        new LanguagePair("el|en", "Du grec vers l'anglais"),
			        new LanguagePair("el|fr", "Du grec vers le français"),
			        new LanguagePair("it|en", "De l'italien vers l'anglais"),
			        new LanguagePair("it|fr", "De l'italien vers le français"),
			        new LanguagePair("ja|en", "Du japonais vers l'anglais"),
			        new LanguagePair("ko|en", "Du coréen vers l'anglais"),
			        new LanguagePair("pt|en", "Du portugais vers l'anglais"),
			        new LanguagePair("pt|fr", "Du portugais vers le français"),
			        new LanguagePair("ru|en", "Du russe vers l'anglais"),
			        new LanguagePair("es|en", "De l'espagnol vers l'anglais"),
			        new LanguagePair("es|fr", "De l'espagnol vers le français")
			    };
			    return lps;
			}
		}
	}
}
