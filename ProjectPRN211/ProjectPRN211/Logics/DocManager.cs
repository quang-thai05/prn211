using ProjectPRN211.DataAccess;
using ProjectPRN211.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

        public List<Document> GetDocuments()
        {
            string sql = "select * from Document doc inner join Hospital hos on doc.hospital_id = hos.hospital_id";
            DataTable dt = DAO.GetDataSql(sql);
            List<Document> list = new List<Document>();
            foreach (DataRow dr in dt.Rows)
            {
                Document doc = new Document();
                doc.DocId = Convert.ToInt32(dr["doc_id"].ToString());
                doc.DocSubject = dr["doc_subject"].ToString();
                doc.DocText = dr["doc_text"].ToString();
                doc.DocDate = Convert.ToDateTime(dr["doc_date"]);
                Hospital hos = new Hospital();
                hos.HospitalId = Convert.ToInt32(dr["hospital_id"]);
                hos.HospitalName = dr["hospital_name"].ToString();
                doc.Hospital = hos;
                list.Add(doc);
            }
            return list;
        }

        public Document GetDocumetById(int id)
        {
            return context.Documents.FirstOrDefault(x => x.DocId == id);
        }
    }
}
