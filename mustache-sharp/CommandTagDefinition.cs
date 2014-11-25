namespace Mustache
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	/// <summary>
	/// 
	/// </summary>
	public class CommandTagDefinition : TagDefinition
	{
		private readonly string _command = null;
		private Func<string, string[], string> _evaluator; 

		public CommandTagDefinition(string command, Func<string, string[], string> evaluator = null)
			: base(command, false)
		{
			_command = command;
			_evaluator = evaluator;
		}

		protected  Func<string, string[], string> Evaluator { get { return _evaluator; } set { _evaluator = value; } }

		/// <summary>
		/// Gets or sets whether the tag can have content.
		/// </summary>
		/// <returns>True if the tag can have a body; otherwise, false.</returns>
		protected override bool GetHasContent()
		{
			return false;
		}

		/// <summary>
		/// Gets the parameters that are used to create a child context.
		/// </summary>
		/// <returns>The parameters that are used to create a child context.</returns>
		public override IEnumerable<TagParameter> GetChildContextParameters()
		{
			return new TagParameter[0];
		}

		public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
		{
			if (_evaluator != null)
				writer.Write(_evaluator(_command, arguments.Values.Cast<string>().ToArray()));
		}
	}
}