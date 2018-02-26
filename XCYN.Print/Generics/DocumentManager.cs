using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Generics
{
    public class DocumentManager<TDocument>
        where TDocument : IDocument
    {
        //如果需要调用泛型类型的方法，就必须给它添加一个约束：TDocument类型必须实现IDocument接口
        private readonly Queue<TDocument> documentQueue = new Queue<TDocument>();

        public void AddDocument(TDocument doc)
        {
            lock(this)
            {
                documentQueue.Enqueue(doc);
            }
        }

        public TDocument GetDocument()
        {
            //泛型default将泛型类型初始化为0或者null
            TDocument doc = default(TDocument);
            lock(this)
            {
                doc = documentQueue.Dequeue();
            }
            return doc;
        }

        public void DisplayAllDocument()
        {
            foreach(TDocument doc in documentQueue)
            {
                Console.WriteLine(doc.Title);
            }
        }
        

        public bool IsDocumentAvailable => documentQueue.Count > 0;
    }

    public interface IDocument
    {
        string Title { get; set; }
        string Content { get; set; }
    }

    public class Document:IDocument
    {
        public static int num;

        public Document()
        {

        }

        public Document(string title,string content)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; set; }
        public string Content { get ; set; }
    }

    public class TStudent<T>
    {
        public static int num;
    }


}
