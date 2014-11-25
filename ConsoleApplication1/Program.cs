using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	using System.Dynamic;
	using System.IO;
    using Mustache;

    class Program
    {
        static void Main(string[] args)
        {
	        const string html = @"{{Form.Name}} {{#! Hello }} {{url|Homepage}}";

            var compiler = new FormatCompiler();
            compiler.RegisterTag(new CommandTagDefinition("url", Url), true);

            var generator = compiler.Compile(html);

	        generator.KeyNotFound += (sender, eventArgs) => Console.WriteLine(eventArgs.Key);

	        var model = new FormPost();
	        model.Form.Name = "Hello";

            string rendered = generator.Render(model);

            Console.ReadKey();
        }

	    public static string Url(string command, string[] arguments)
	    {
		    return "/homepage";
	    }

	    public class FormPost
	    {
			private ExpandoObject _expando = new ExpandoObject();

			public dynamic Form { get { return _expando; }}
	    }
    }
}
