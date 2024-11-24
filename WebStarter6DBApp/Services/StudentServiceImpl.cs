using System.Transactions;
using AutoMapper;
using WebStarter6DBApp.DAO;
using WebStarter6DBApp.DTO;
using WebStarter6DBApp.Exceptions;
using WebStarter6DBApp.Models;

namespace WebStarter6DBApp.Services
{
    public class StudentServiceImpl : IStudentService
    {
        // Inject dao, mapper and logger
        private readonly IStudentDAO _studentDAO;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentServiceImpl> _logger;

        // In order for the constructor to be used, dao, mapper and logger must be in the IOC container.
        // We have added them in Program.cs
        public StudentServiceImpl(IStudentDAO studentDAO, IMapper mapper, ILogger<StudentServiceImpl> logger)
        {
            _studentDAO = studentDAO;
            _mapper = mapper;
            _logger = logger;
        }

        public StudentReadOnlyDTO? InsertStudent(StudentInsertDTO studentInsertDTO)
        {
            StudentReadOnlyDTO studentReadOnlyDTO;
            try
            {
                using TransactionScope scope = new TransactionScope();  // Begin of transaction

                // We should check if student to be inserted is unique  

                Student student = _mapper.Map<Student>(studentInsertDTO);
                Student? insertedStudent = _studentDAO.Insert(student);
                studentReadOnlyDTO = _mapper.Map<StudentReadOnlyDTO>(insertedStudent);

                scope.Complete();                                       // End of transaction

                return studentReadOnlyDTO;
            }
            catch (TransactionException ex)
            {
                // In case of TransactionException there will be an automatic rollback.
                _logger.LogError("Error. Student {Firstname} {Lastname} not inserted. {ErrorMessage}",
                    studentInsertDTO.Firstname, studentInsertDTO.Lastname, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                // rollback
                _logger.LogError("Error. Student {Firstname} {Lastname} not inserted. {ErrorMessage}",
                    studentInsertDTO.Firstname, studentInsertDTO.Lastname, ex.Message);
                throw;
            }
        }
        public void UpdateStudent(StudentUpdateDTO studentUpdateDTO)
        {
            Student student;

            try
            {
                using TransactionScope scope = new();

                if (_studentDAO.GetById(studentUpdateDTO.Id) == null)
                {
                    throw new StudentNotFoundException($"Student with id {studentUpdateDTO.Id} not found.");
                }

                student = _mapper.Map<Student>(studentUpdateDTO);
                _studentDAO.Update(student);
                scope.Complete();
            } 
            catch (StudentNotFoundException ex)
            {
                _logger.LogError("Error. Student with Id: {Id} not found. {ErrorMessage}.", studentUpdateDTO.Id, ex.Message);
                throw;
            } 
            catch (TransactionException ex)
            {
                _logger.LogError("Error. Student {Firstname} {Lastname} was not updated. {ErrorMessage}.",
                    studentUpdateDTO.Firstname, studentUpdateDTO.Lastname, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error. Student {Firstname} {Lastname} could not be updated. {ErrorMessage}.",
                   studentUpdateDTO.Firstname, studentUpdateDTO.Lastname, ex.Message);
                throw;
            }
        }
        
        public void DeleteStudent(int id)
        {
            try
            {
                using TransactionScope scope = new();
                if (_studentDAO.GetById(id) == null)
                {
                    throw new StudentNotFoundException($"Student with Id: {id} was not found");
                }
                _studentDAO.Delete(id);
                scope.Complete();
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError("Error. Student with Id: {Id} was not found. {ErrorMessage}.", id, ex.Message);
                throw;
            }
            catch (TransactionException ex)
            {
                _logger.LogError("Error. Student with id {Id} was not found. {ErrorMessage}.", id, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error. Student with id {Id} could not be deleted. {ErrorMessage}.", id, ex.Message);
                throw;
            }
        }

        public StudentReadOnlyDTO? GetStudent(int id)
        {
            StudentReadOnlyDTO studentReadOnlyDTO;
            Student? student;

            try
            {
                // No need for transaction here. We have a SELECT statement.
                student = _studentDAO.GetById(id);
                if (student == null)
                {
                    throw new StudentNotFoundException($"Student with id {id} not found.");
                }
                studentReadOnlyDTO = _mapper.Map<StudentReadOnlyDTO>(student);
                return studentReadOnlyDTO;
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError("Error. Student with Id: {Id} was not found. {ErrorMessage}.", id, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error. Student with id {Id} could not be deleted. {ErrorMessage}.", id, ex.Message);
                throw;
            }
        }

        public List<StudentReadOnlyDTO> GetAllStudents()
        {
            StudentReadOnlyDTO studentReadOnlyDTO;
            List<StudentReadOnlyDTO> studentReadOnlyDTOs = new();
            List<Student> students;

            try
            {
                students = _studentDAO.GetAll();

                foreach(Student student in students)
                {
                    studentReadOnlyDTO = _mapper.Map<StudentReadOnlyDTO>(student);
                    studentReadOnlyDTOs.Add(studentReadOnlyDTO);
                }
                return studentReadOnlyDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error. Students could not be returned. {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
