using System;
using System.Collections.Generic;
using System.Linq;
using Fakultet.Models;

namespace Fakultet.Data
{
    public class SQLFakultetRepository : IFakultetRepository
    {
        private readonly FakultetContext _context;

        public SQLFakultetRepository(FakultetContext context)
        {
            _context = context;
        }

        public List<Predmet> GetAllPredmets()
        {
            return _context.Predmeti.ToList();
        }

        public List<Student> GetAllStudents()
        {
            return _context.Studenti.ToList();
        }

        public Predmet GetPredmet(int id)
        {
            return _context.Predmeti.FirstOrDefault(p => p.Id == id);
        }

        public Student GetStudent(int index)
        {
            return _context.Studenti.FirstOrDefault(p => p.Index == index);
        }

        public List<Predmet> GetPredmetiStudenta(int index)
        {
            List<int> idPredmeta = _context.StudentPredmet.Where(s => s.IndexStudenta == index)
            .Select(p => p.IdPredmeta).ToList();
            List<Predmet> predmeti = new List<Predmet>();
            foreach (int i in idPredmeta)
            {
                predmeti.Add(GetPredmet(i));
            }
            return predmeti;
        }

        public List<Student> GetStudentiPredmeta(int index)
        {
            List<int> idStudenta = _context.StudentPredmet.Where(s => s.IdPredmeta == index)
            .Select(p => p.IndexStudenta).ToList();
            List<Student> studenti = new List<Student>();
            foreach (int i in idStudenta)
            {
                studenti.Add(GetStudent(i));
            }
            return studenti;
        }


        public void AddStudent(Student s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            _context.Studenti.Add(s);
        }

        public void AddPredmet(Predmet p)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }
            _context.Predmeti.Add(p);
        }

        public void UpdateStudent(Student s)
        {
            //sfs
        }

        public void UpdatePredmet(Predmet p)
        {
            //sffs
        }

        public void DeleteStudent(Student s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            _context.Studenti.Remove(s);
            DeleteSvePredmeteStudenta(s);
        }

        public void DeletePredmet(Predmet p)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }
            _context.Predmeti.Remove(p);
            DeleteSveStudenteSaPredmeta(p);
        }


        //brise svako pojavljivanje studenta s u tabeli veze StudentPredmet
        public void DeleteSvePredmeteStudenta(Student s)
        {
            if (s == null)
            {
                throw new ArgumentException(nameof(s));
            }
            List<StudentPredmet> studentiPredmeti = _context.StudentPredmet.Where(t => t.IndexStudenta == s.Index).ToList();
            foreach (StudentPredmet i in studentiPredmeti)
            {
                _context.StudentPredmet.Remove(i);
            }

        }

        //brise svako pojavljivanje predmeta p u tabeli veze StudentPredmet
        public void DeleteSveStudenteSaPredmeta(Predmet p)
        {
            if (p == null)
            {
                throw new ArgumentException(nameof(p));
            }
            List<StudentPredmet> studentiPredmeti = _context.StudentPredmet.Where(s => s.IdPredmeta == p.Id).ToList();
            foreach (StudentPredmet i in studentiPredmeti)
            {
                _context.StudentPredmet.Remove(i);
            }
        }

        public void AddPredmetStudentu(Predmet p, Student s)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }
            _context.StudentPredmet.Add(new StudentPredmet(s.Index, p.Id));
        }

        public void AddStudentaPredmetu(Student s, Predmet p)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            _context.StudentPredmet.Add(new StudentPredmet(s.Index, p.Id));
        }

        public void IspisiStudentaSaPredmeta(Student s, Predmet p)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }
            StudentPredmet sp = _context.StudentPredmet.FirstOrDefault(e => e.IndexStudenta == s.Index && e.IdPredmeta == p.Id);
            _context.StudentPredmet.Remove(sp);
        }



        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}