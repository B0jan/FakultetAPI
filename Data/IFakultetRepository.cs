using System.Collections.Generic;
using Fakultet.Models;

namespace Fakultet.Data
{
    public interface IFakultetRepository
    {
        bool SaveChanges();

        List<Predmet> GetPredmetiStudenta(int index);
        List<Student> GetStudentiPredmeta(int index);

        Student GetStudent(int index);
        Predmet GetPredmet(int id);

        List<Student> GetAllStudents();
        List<Predmet> GetAllPredmets();

        void AddStudent(Student s);
        void AddPredmet(Predmet p);

        void AddPredmetStudentu(Predmet p, Student s);
        void AddStudentaPredmetu(Student s, Predmet p);

        void UpdateStudent(Student s);
        void UpdatePredmet(Predmet p);

        void DeleteStudent(Student s);
        void DeletePredmet(Predmet p);

        void IspisiStudentaSaPredmeta(Student s, Predmet p);

        void DeleteSveStudenteSaPredmeta(Predmet p);
        void DeleteSvePredmeteStudenta(Student s);
    }
}