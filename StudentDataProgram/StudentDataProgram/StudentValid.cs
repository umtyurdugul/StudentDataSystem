using FluentValidation;
using System.Text.RegularExpressions;

namespace StudentDataProgram
{
    internal class StudentValid : AbstractValidator<Student>
    {

        public  StudentValid()
        {

            Regex regexNameSurname = new Regex(@"a-zA-Z");
            
           


            RuleFor(x => x.studentNumber).NotEmpty().NotNull();
            RuleFor(x => x.name).NotEmpty().WithMessage("Lütfen bir isim giriniz!").Matches(regexNameSurname).WithMessage("Lütfen geçerli bir isim giriniz!");
            RuleFor(x => x.lastName).NotEmpty().WithMessage("Lütfen bir soyisim giriniz!").Matches(regexNameSurname).WithMessage("Lütfen geçerli bir soyisim giriniz!") ;
            RuleFor(x => x.grade).NotNull().WithMessage("Lütfen bir sınıf seçiniz");
            RuleFor(x => x.univercity).NotNull().WithMessage("Lütfen bir üniversite seçiniz");
            RuleFor(x => x.department).NotNull().WithMessage("Lütfen bir bölüm seçiniz");

        }



    }
}
