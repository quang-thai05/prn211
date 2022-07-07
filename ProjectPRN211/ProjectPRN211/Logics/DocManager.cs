using ProjectPRN211.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPRN211.Logics
{
    public class DocManager
    {
        PRN211_ProjectContext context;

        public DocManager()
        {
            context = new PRN211_ProjectContext();
        }

        public List<Document> GetDocs()
        {
            return context.Documents.ToList();
        }

        public void CreateDoc(Document doc)
        {
            context.Add(doc);
            context.SaveChanges();
        }

        public void UpdateDoc(Document document, int id)
        {
            Document doc = context.Documents.FirstOrDefault(x => x.DocId == id);
            doc.DocSubject = document.DocSubject;
            doc.DocText = document.DocText;
            doc.DocDate = document.DocDate;
            doc.HospitalId = document.HospitalId;
            context.SaveChanges();
        }

        public void DeleteDoc(int id)
        {
            Document doc = context.Documents.FirstOrDefault(x => x.DocId == id);
            context.Remove(doc);
            context.SaveChanges();
        }
    }
}
