using System.Collections.Generic;
using AutoMapper;
using Fakultet.Data;
using Fakultet.Dtos;
using Fakultet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Fakultet.Controllers
{
    [Route("api/fakultets")]
    [ApiController]
    public class FakultetsController : ControllerBase
    {
        private readonly IFakultetRepository _repository;
        private readonly IMapper _mapper;

        public FakultetsController(IFakultetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET
        //http://localhost:5000/api/fakultets/predmetistudenta/<indeksStudenta>
        [HttpGet("predmetistudenta/{indeks}")]
        public ActionResult<List<PredmetReadDto>> GetStudentPredmets(int indeks)
        {
            var listaPredmeta = _repository.GetPredmetiStudenta(indeks);
            if (listaPredmeta != null)
            {
                return Ok(_mapper.Map<List<PredmetReadDto>>(listaPredmeta));
            }
            return NotFound();
        }

        //GET
        //http://localhost:5000/api/fakultets/studentisapredmeta/<idPredmeta>
        [HttpGet("studentisapredmeta/{id}")]
        public ActionResult<List<StudentReadDto>> GetStudentiSaPredmeta(int id)
        {
            var listaStudenta = _repository.GetStudentiPredmeta(id);
            if (listaStudenta != null)
            {
                return Ok(_mapper.Map<List<StudentReadDto>>(listaStudenta));
            }
            return NotFound();
        }

        //GET
        //http://localhost:5000/api/fakultets/student/<indeksStudenta>
        [Route("student")]
        [HttpGet("student/{indeks}", Name = "GetStudent")]
        public ActionResult<StudentReadDto> GetStudent(int indeks)
        {
            Student student = _repository.GetStudent(indeks);
            if (student != null)
            {
                return Ok(_mapper.Map<StudentReadDto>(student));
            }
            return NotFound();
        }

        //GET
        //http://localhost:5000/api/fakultets/predmet/<idPredmeta>
        [Route("predmet")]
        [HttpGet("predmet/{id}", Name = "GetPredmet")]
        public ActionResult<PredmetReadDto> GetPredmet(int id)
        {
            Predmet predmet = _repository.GetPredmet(id);
            if (predmet != null)
            {
                return Ok(_mapper.Map<PredmetReadDto>(predmet));
            }
            return NotFound();
        }

        //GET
        //http://localhost:5000/api/fakultets/student
        [Route("student")]
        [HttpGet]
        public ActionResult<List<StudentReadDto>> GetStudents()
        {
            List<Student> studenti = _repository.GetAllStudents();
            if (studenti != null)
            {
                return Ok(_mapper.Map<List<StudentReadDto>>(studenti));
            }
            return NotFound();
        }

        //GET
        //http://localhost:5000/api/fakultets/predmet
        [Route("predmet")]
        [HttpGet]
        public ActionResult<List<PredmetReadDto>> GetPredmets()
        {
            List<Predmet> predmeti = _repository.GetAllPredmets();
            if (predmeti != null)
            {
                return Ok(_mapper.Map<List<PredmetReadDto>>(predmeti));
            }
            return NotFound();
        }


        //POST u body unos JSON-a
        //http://localhost:5000/api/fakultets/student
        [Route("student")]
        [HttpPost]
        public ActionResult<StudentReadDto> AddStudent(StudentCreateDto student)
        {
            var studentModel = _mapper.Map<Student>(student);
            _repository.AddStudent(studentModel);
            _repository.SaveChanges();

            var studentReadDto = _mapper.Map<StudentReadDto>(studentModel);

            return CreatedAtRoute(nameof(GetStudent), new { indeks = studentModel.Index }, studentReadDto);
        }

        //POST u body unos JSON-a
        //http://localhost:5000/api/fakultets/predmet
        [Route("predmet")]
        [HttpPost]
        public ActionResult<PredmetReadDto> AddPredmet(PredmetCreateDto predmet)
        {
            var predmetModel = _mapper.Map<Predmet>(predmet);
            _repository.AddPredmet(predmetModel);
            _repository.SaveChanges();

            var predmetReadDto = _mapper.Map<PredmetReadDto>(predmetModel);

            return CreatedAtRoute(nameof(GetPredmet), new { Id = predmetModel.Id }, predmetReadDto);
        }

        //POST
        //http://localhost:5000/api/fakultets/student/<indeks>/predmet/<idPredmeta>
        [HttpPost("student/{indeks}/predmet/{idPredmeta}")]
        public ActionResult AddStudentaNaPredmet(int indeks, int idPredmeta)
        {
            Student s = _repository.GetStudent(indeks);
            Predmet p = _repository.GetPredmet(idPredmeta);
            if (s == null && p == null)
            {
                return NotFound();
            }
            _repository.AddStudentaPredmetu(s, p);
            _repository.SaveChanges();
            return NoContent();
        }

        //POST
        //http://localhost:5000/api/fakultets/predmet/<idPredmeta>/student/<indeks>
        [HttpPost("predmet/{idPredmeta}/student/{indeks}")]
        public ActionResult AddPredmetStudentu(int idPredmeta, int indeks)
        {
            Predmet p = _repository.GetPredmet(idPredmeta);
            Student s = _repository.GetStudent(indeks);
            if (p == null && s == null)
            {
                return NotFound();
            }
            _repository.AddPredmetStudentu(p, s);
            _repository.SaveChanges();
            return NoContent();
        }


        //PUT izmena entiteta preko EF u body unos JSON-a
        //http://localhost:5000/api/fakultets/student/<indeksStudenta>
        [Route("student")]
        [HttpPut("student/{id}")]
        public ActionResult UpdateStudent(int id, StudentUpdateDto studentUpdateDto)
        {
            Student studentFromBase = _repository.GetStudent(id);
            if (studentFromBase == null)
            {
                return NotFound();
            }
            //Entity Framework update
            _mapper.Map(studentUpdateDto, studentFromBase);
            _repository.UpdateStudent(studentFromBase);
            _repository.SaveChanges();

            return NoContent();
        }

        //PUT izmena preko EF u body unos JSON-a
        //http://localhost:5000/api/fakultets/predmet/<idPredmeta>
        [Route("predmet")]
        [HttpPut("predmet/{id}")]
        public ActionResult UpdatePredmet(int id, PredmetUpdateDto predmetUpdateDto)
        {
            Predmet predmetFromBase = _repository.GetPredmet(id);
            if (predmetFromBase == null)
            {
                return NotFound();
            }
            _mapper.Map(predmetUpdateDto, predmetFromBase);
            _repository.UpdatePredmet(predmetFromBase);
            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH izmena dela entiteta preko EF u body unos JSON komande
        //http://localhost:5000/api/fakultets/student/<indeksStudenta>

        /*JSON komanda
        {
            "op": "replace",
            "path": "kolonaZaUpdate",
            "value": "novaVrednost"
        }
        */
        [Route("student")]
        [HttpPatch("student/{id}")]
        public ActionResult PartialUpdateStudent(int id, JsonPatchDocument<StudentUpdateDto> patchStudent)
        {
            Student studentFromBase = _repository.GetStudent(id);
            if (studentFromBase == null)
            {
                return NotFound();
            }
            var studentToPatch = _mapper.Map<StudentUpdateDto>(studentFromBase);
            patchStudent.ApplyTo(studentToPatch, ModelState);
            if (!TryValidateModel(studentToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(studentToPatch, studentFromBase);
            _repository.UpdateStudent(studentFromBase);
            _repository.SaveChanges();
            return NoContent();
        }

        //PATCH izmena dela entiteta preko EF u body unos JSON komande
        //http://localhost:5000/api/fakultets/predmet/<idPredmeta>

        /*JSON komanda
        {
            "op": "replace",
            "path": "kolonaZaUpdate",
            "value": "novaVrednost"
        }
        */
        [Route("predmet")]
        [HttpPatch("predmet/{id}")]
        public ActionResult PartialUpdatePredmet(int id, JsonPatchDocument<PredmetUpdateDto> patchPredmet)
        {
            Predmet predmetFromBase = _repository.GetPredmet(id);
            if (predmetFromBase == null)
            {
                return NotFound();
            }
            var predmetToPatch = _mapper.Map<PredmetUpdateDto>(predmetFromBase);
            patchPredmet.ApplyTo(predmetToPatch, ModelState);
            if (!TryValidateModel(predmetToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(predmetToPatch, predmetFromBase);
            _repository.UpdatePredmet(predmetFromBase);
            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE
        //http://localhost:5000/api/fakultets/student/<indeksStudenta>
        [Route("student")]
        [HttpDelete("student/{id}")]
        public ActionResult DeleteStudent(int id)
        {
            Student studentFromBase = _repository.GetStudent(id);
            if (studentFromBase == null)
            {
                return NotFound();
            }
            _repository.DeleteStudent(studentFromBase);
            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE
        //http://localhost:5000/api/fakultets/predmet/<idPredmeta>
        [Route("predmet")]
        [HttpDelete("predmet/{id}")]
        public ActionResult DeletePredmet(int id)
        {
            Predmet predmetFromBase = _repository.GetPredmet(id);
            if (predmetFromBase == null)
            {
                return NotFound();
            }
            _repository.DeletePredmet(predmetFromBase);
            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE
        //http://localhost:5000/api/fakultets/student/<indeks>/predmet/<idPredmeta>
        [HttpDelete("student/{indeks}/predmet/{idPredmeta}")]
        public ActionResult IspisiStudentaSaPredmeta(int indeks, int idPredmeta)
        {
            Student s = _repository.GetStudent(indeks);
            Predmet p = _repository.GetPredmet(idPredmeta);
            _repository.IspisiStudentaSaPredmeta(s, p);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}