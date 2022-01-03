using OOP_lab1;
using System;
using System.Collections.Generic;

namespace HomeWorkOOP1
{
	class Program
	{
		static void Main(string[] args)
		{
			Person me = new Person("Стас", Sex.male);
			Person father = new Person("Анатолий", Sex.male);
			Person mother = new Person("Юлия", Sex.female);
			father.SetMarriedValue(mother);
			father.AddChild(me);

			Person fatherByFather = new Person("Валерий", Sex.male);
			Person motherByFather = new Person("Наталия Глебовна", Sex.female);
			Person fatherBrother = new Person("Иван", Sex.male);
			Person fatherBrotherWife = new Person("Наталия", Sex.female);
			Person fatherBrotherSon = new Person("Макар", Sex.male);

			fatherByFather.SetMarriedValue(motherByFather);
			fatherByFather.AddChild(father);
			fatherByFather.AddChild(fatherBrother);

			fatherBrother.SetMarriedValue(fatherBrotherWife);
			fatherBrother.AddChild(fatherBrotherSon);


			Person fatherByMother = new Person("Виктор", Sex.male);
			Person motherByMother = new Person("Наталия Дмитриевна", Sex.female);
			Person motherBrother = new Person("Константин", Sex.male);
			Person motherBrotherWife = new Person("Юлия Константиновна", Sex.female);
			Person motherBrotherSon1 = new Person("Илья", Sex.male);
			Person motherBrotherSon2 = new Person("Андрей", Sex.male);
			Person motherBrotherDaughter = new Person("Анна", Sex.female);

			fatherByMother.SetMarriedValue(motherByMother);
			fatherByMother.AddChild(mother);
			fatherByMother.AddChild(motherBrother);

			motherBrother.SetMarriedValue(motherBrotherWife);
			motherBrother.AddChild(motherBrotherSon1);
			motherBrother.AddChild(motherBrotherSon2);
			motherBrother.AddChild(motherBrotherDaughter);

			motherBrotherSon1.PrintNamesOfParents();
			motherBrotherSon1.PrintUncles();
			motherBrotherSon1.PrintAunties();
			motherBrotherSon1.PrintCousins();
			motherBrotherSon1.PrintInLaws();

		}
	}
}
