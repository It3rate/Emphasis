using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using java.io;
using edu.stanford.nlp.process;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.trees;
using edu.stanford.nlp.parser.lexparser;
using Console = System.Console;
using edu.stanford.nlp.pipeline;

namespace Empahsis
{
	class StanfordNLP
	{
		private LexicalizedParser lp;
		private TokenizerFactory tokenizerFactory;
		private PennTreebankLanguagePack tlp;
		private GrammaticalStructureFactory gsf;
		private StanfordCoreNLP docPipeline;

		const string localRoot = "dicts";
		const string modelsDirectory = localRoot + "\\stanford";
		const string jarRoot = @"D:\store\stanfordNLP\stanford-parser-full-2018-10-17\stanford-parser-3.9.2-models";

		public StanfordNLP()
		{
			// Path to models extracted from `stanford-parser-3.8.0-models.jar`

			//runSamples();
		}

		public void setupSentence()
		{
			// Loading english PCFG parser from file
			lp = LexicalizedParser.loadModel(modelsDirectory + "\\englishPCFG.ser.gz");
			tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
			tlp = new PennTreebankLanguagePack();
			gsf = tlp.grammaticalStructureFactory();
		}
		public void setupDoc()
		{
			var props = new java.util.Properties();
			// props.setProperty("annotators", "tokenize, ssplit, pos, lemma, ner, parse, dcoref");
			props.setProperty("annotators", "tokenize, ssplit, pos, lemma, parse");
			props.setProperty("ner.useSUTime", "0");

			var curDir = Environment.CurrentDirectory;
			System.IO.Directory.SetCurrentDirectory(jarRoot);
			docPipeline = new StanfordCoreNLP(props);
			System.IO.Directory.SetCurrentDirectory(curDir);
		}

		public string parseLines(string doc)
		{
			return parse(doc);
		}

		public string parseDoc(string doc)
		{
			string result = "";

			// Annotation
			var annotation = new Annotation(doc);
			docPipeline.annotate(annotation);

			// Result - Pretty Print
			using (var stream = new ByteArrayOutputStream())
			{
				docPipeline.prettyPrint(annotation, new PrintWriter(stream));
				result = stream.toString();
				stream.close();
			}
			return result;
		}

		public string parse(string sentence)
		{
			string result = "";
			var sent2Reader = new StringReader(sentence);
			var rawWords2 = this.tokenizerFactory.getTokenizer(sent2Reader).tokenize();
			sent2Reader.close();
			var tree2 = lp.apply(rawWords2);
			var gs = gsf.newGrammaticalStructure(tree2);
			var tdl = gs.typedDependenciesCCprocessed();
			result = string.Format("\n{0}\n", tdl);
			return result;
		}

		void runSamples()
		{
			// This sample shows parsing a list of correctly tokenized words
			var sent = new[] { "This", "is", "an", "easy", "sentence", "." };
			var rawWords = SentenceUtils.toCoreLabelList(sent);
			var tree = lp.apply(rawWords);
			tree.pennPrint();

			// This option shows loading and using an explicit tokenizer
			var sent2 = "This is another sentence.";
			var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
			var sent2Reader = new StringReader(sent2);
			var rawWords2 = tokenizerFactory.getTokenizer(sent2Reader).tokenize();
			sent2Reader.close();
			var tree2 = lp.apply(rawWords2);

			// Extract dependencies from lexical tree
			var tlp = new PennTreebankLanguagePack();
			var gsf = tlp.grammaticalStructureFactory();
			var gs = gsf.newGrammaticalStructure(tree2);
			var tdl = gs.typedDependenciesCCprocessed();
			System.Console.WriteLine("\n{0}\n", tdl);

			// Extract collapsed dependencies from parsed tree
			var tp = new TreePrint("penn,typedDependenciesCollapsed");
			tp.printTree(tree2);
		}

	}
}
