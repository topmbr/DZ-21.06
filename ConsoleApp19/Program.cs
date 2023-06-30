public class Student
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public int Year { get; set; }

    public Student(string name, string surname, int age, int year)
    {
        Name = name;
        Surname = surname;
        Age = age;
        Year = year;
    }

    public override string ToString()
    {
        return $"{Name} {Surname} (Age: {Age}, Year: {Year})";
    }
}

public class StudentManager
{
    private List<Student> students;

    public StudentManager()
    {
        students = new List<Student>();
    }

    public void AddStudent(Student student)
    {
        students.Add(student);
        Console.WriteLine("Студент успешно добавлен.");
    }

    public void RemoveStudent(Student student)
    {
        students.Remove(student);
        Console.WriteLine("Студент успешно удален.");
    }

    public List<Student> GetStudentsByYear(int year)
    {
        List<Student> filteredStudents = students.FindAll(student => student.Year == year);
        return filteredStudents;
    }

    public Student GetOldestStudent()
    {
        Student oldestStudent = null;
        int maxAge = 0;

        foreach (var student in students)
        {
            if (student.Age > maxAge)
            {
                maxAge = student.Age;
                oldestStudent = student;
            }
        }

        return oldestStudent;
    }
}

public class Program
{
    private static StudentManager studentManager;

    static void Main(string[] args)
    {
        studentManager = new StudentManager();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine();
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Удалить студента");
            Console.WriteLine("3. Получить студентов по годам");
            Console.WriteLine("4. Получить старшего ученика");
            Console.WriteLine("5. Выход");
            Console.WriteLine();

            Console.Write("Введите свой выбор (1-5): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    RemoveStudent();
                    break;
                case "3":
                    GetStudentsByYear();
                    break;
                case "4":
                    GetOldestStudent();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте еще раз.");
                    break;
            }
        }
    }

    static void AddStudent()
    {
        Console.WriteLine("Введите данные студента:");
        Console.Write("Имя: ");
        string name = Console.ReadLine();
        Console.Write("Фамилия: ");
        string surname = Console.ReadLine();
        Console.Write("Возраст: ");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age) || age < 0)
        {
            Console.WriteLine("Неверный Ввод. Пожалуйста, введите положительное целое число для возраста.");
            Console.Write("Возраст: ");
        }
        Console.Write("Год рождения: ");
        int year;
        while (!int.TryParse(Console.ReadLine(), out year) || year < 0)
        {
            Console.WriteLine("Неверный Ввод. Пожалуйста, введите положительное целое число для года.");
            Console.Write("Год рождения: ");
        }

        Student student = new Student(name, surname, age, year);
        studentManager.AddStudent(student);
    }

    static void RemoveStudent()
    {
        Console.WriteLine("Введите данные студента, чтобы удалить:");
        Console.Write("Имя: ");
        string name = Console.ReadLine();
        Console.Write("Фамилия: ");
        string surname = Console.ReadLine();

        List<Student> matchingStudents = studentManager.GetStudentsByYear(1).FindAll(student => student.Name == name && student.Surname == surname);
        if (matchingStudents.Count > 0)
        {
            Console.WriteLine("Найдены несколько учеников. Пожалуйста, выберите студента для удаления:");

            for (int i = 0; i < matchingStudents.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {matchingStudents[i].ToString()}");
            }

            Console.Write("Введите индекс студента, которого нужно удалить: ");
            int index;
            while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > matchingStudents.Count)
            {
                Console.WriteLine("Неверный Ввод. Пожалуйста, введите действительный индекс.");
                Console.Write("Введите индекс студента, которого нужно удалить: ");
            }

            Student studentToRemove = matchingStudents[index - 1];
            studentManager.RemoveStudent(studentToRemove);
        }
        else
        {
            Console.WriteLine("Студент не найден.");
        }
    }

    static void GetStudentsByYear()
    {
        Console.Write("Введите год, чтобы отфильтровать учащихся: ");
        int year;
        while (!int.TryParse(Console.ReadLine(), out year) || year < 0)
        {
            Console.WriteLine("Неверный Ввод. Пожалуйста, введите положительное целое число для года.");
            Console.Write("Введите год, чтобы отфильтровать учащихся: ");
        }

        List<Student> filteredStudents = studentManager.GetStudentsByYear(year);
        if (filteredStudents.Count > 0)
        {
            Console.WriteLine($"Студенты в году {year}:");
            foreach (var student in filteredStudents)
            {
                Console.WriteLine(student.ToString());
            }
        }
        else
        {
            Console.WriteLine("Студентов за данный год не найдено.");
        }
    }

    static void GetOldestStudent()
    {
        Student oldestStudent = studentManager.GetOldestStudent();
        if (oldestStudent != null)
        {
            Console.WriteLine("Самый старший ученик:");
            Console.WriteLine(oldestStudent.ToString());
        }
        else
        {
            Console.WriteLine("Студенты не найдены.");
        }
    }
}
