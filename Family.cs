using System;
using System.Collections.Generic;

namespace OOP_lab1
{

	enum Sex { male, female };

	class Person
	{
		public string name;
		private Sex sex;
		private Person dad;
		private Person mom;
		public Person partner;
		private List<Person> child;

		public Person(string name, Sex sex)
		{
			this.name = name;
			this.sex = sex;
		}

		public void SetMarriedValue(Person m_partner)
		{
			if (CheckPersonNotNull(m_partner))
			{
				if (CheckPersonNotNull(m_partner.partner))
				{
					m_partner.partner.partner = null;
				}
				if (CheckPersonNotNull(partner))
				{
					partner.partner = null;
				}
				partner = m_partner;
				m_partner.partner = this;
			}
			else
			{
				Console.WriteLine("Ошибка в SetMarriedValue");
			}
		}

		private Boolean CheckPersonNotNull(Person p)
		{
			if (p != null)
			{
				return true;
			}
			return false;
		}

		private Boolean CheckThatArrayExist(List<Person> list)
		{
			if (list != null)
			{
				return true;
			}
			return false;
		}

		private void PrintNothing(String message)
		{
			Console.WriteLine($"По данному запросу - {message} - ничего не было найдено.");
		}

		private void SetChild(Person child)
		{
			if (CheckThatArrayExist(child.child))
			{
				if (child.child.Contains(this))
				{
					child.child.RemoveAt(child.child.IndexOf(this));
				}
			}
			if (CheckThatArrayExist(this.child))
			{
				this.child.Add(child);
			}
			else
			{
				List<Person> children = new List<Person>();
				children.Add(child);
				this.child = children;
			}
		}

		public void AddChild(Person child)
		{
			if (CheckPersonNotNull(child)){
				SetChild(child);
				if (CheckPersonNotNull(partner))
				{
					partner.SetChild(child);
				}
				switch (sex)
				{
					case Sex.female:
						child.mom = this;
						child.dad = partner;
						break;
					case Sex.male:
						child.dad = this;
						child.mom = partner;
						break;
				}
			}
			else
			{
				Console.WriteLine("Ошибка в AddChild");
			}
		}

		public void PrintNamesOfParents()
		{
			if (!(CheckPersonNotNull(dad)) && !(CheckPersonNotNull(mom)))
			{
				PrintNothing($"{name} PrintNamesOfParents");
			}
			else
			{
				if (CheckPersonNotNull(dad)) { Console.WriteLine($"У {name} папа: {dad.name}"); }
				if (CheckPersonNotNull(mom)) { Console.WriteLine($"У {name} мама: {mom.name}"); }
			}
		}

		private List<Person> GetSiblings(Person person, Sex sex)
		{
			Person parent = CheckAndSetParent(person);
			if (!(CheckPersonNotNull(parent)))
			{
				return null;
			}
			List<Person> list = new List<Person>();
			foreach (Person p in parent.child)
			{
				if (p != person)
				{
					if (p.sex == sex)
					{
						list.Add(p);
					}
					else
					{
						if (CheckPersonNotNull(p.partner))
						{
							list.Add(p.partner);
						}
					}
				}
			}
			return list;
		}

		private List<Person> GetPeople(Sex sex)
		{
			if (!(CheckPersonNotNull(dad)) && !(CheckPersonNotNull(mom))){
				return null;
			}
			List<Person> list = new List<Person>();
			List<Person> sublist = new List<Person>();
			List<Person> sublist1 = new List<Person>();
			if (CheckPersonNotNull(dad))
			{
				sublist = (GetSiblings(dad, sex));
				if (CheckThatArrayExist(sublist)) { list.AddRange(sublist); }
			}
			if (CheckPersonNotNull(mom))
			{
				sublist1 = (GetSiblings(mom, sex));
				if (CheckThatArrayExist(sublist1)) { list.AddRange(sublist1); }
			}
			return list;
		}

		public void PrintUncles() {
			List<Person> list = GetPeople(Sex.male);
			if (CheckThatArrayExist(list)) {
				foreach (Person uncles in list) {
					Console.WriteLine($"У {name} дядя: {uncles.name}");
				}
			}
			else
			{
				PrintNothing($"{name} PrintUncles");
			}
		}

		public void PrintAunties()
		{
			List<Person> list = GetPeople(Sex.female);
			if (CheckThatArrayExist(list))
			{
				foreach (Person aunties in list)
				{
					Console.WriteLine($"У {name} тетя: {aunties.name}");
				}
			}
			else
			{
				PrintNothing($"{name} PrintAunties");
			}
		}

		private Person CheckAndSetParent(Person p)
		{
			Person person = null;
			if (CheckPersonNotNull(p.dad)) { person = p.dad;}
			else if (CheckPersonNotNull(p.mom)) { person = p.mom; }
			return person;
		}

		private List<Person> GetCousinsFrom(Person person)
		{
			List<Person> cousins_list = new List<Person>();
			Person parent = CheckAndSetParent(person);
			if (CheckPersonNotNull(parent))
			{
				foreach (Person human in parent.child)
				{
					if (CheckThatArrayExist(human.child) && (human != dad && human != mom))
					{
						cousins_list.AddRange(human.child);
					}
				}
			}
			else { return null; }
			return cousins_list;
		}

		private List<Person> GetAllCousins()
		{
			List<Person> cousin_list = new List<Person>();
			List<Person> momLine = new List<Person>();
			List<Person> dadLine = new List<Person>();

			if (!(CheckPersonNotNull(mom)) && !(CheckPersonNotNull(dad))){
				return null;
			}
			if (CheckPersonNotNull(mom)) {
				momLine = GetCousinsFrom(mom);
				if (CheckThatArrayExist(momLine))
				{
					cousin_list.AddRange(momLine);
				}
			}
			if (CheckPersonNotNull(dad)) {
				dadLine = GetCousinsFrom(dad);
				if (CheckThatArrayExist(dadLine))
				{
					cousin_list.AddRange(dadLine);
				}
			}
			return cousin_list;
		}

		public void PrintCousins()
		{
			List<Person> list = GetAllCousins();
			if (CheckThatArrayExist(list))
			{
				foreach (Person cousin in list)
				{
					Console.WriteLine($"У {name} cousin: {cousin.name}");
				}
			}
			else
			{
				PrintNothing($"{name} PrintCousins");
			}
		}

		private List<Person> GetInLaws()
		{
			List<Person> laws_list = new List<Person>();
			if (CheckPersonNotNull(partner))
			{
				if (!(CheckPersonNotNull(mom)) && (!CheckPersonNotNull(dad))) {
					return null;
				}
				if (CheckPersonNotNull(partner.dad)) { laws_list.Add(partner.dad); }
				if (CheckPersonNotNull(partner.mom)) { laws_list.Add(partner.mom); }
			}
			else
			{
				return null;
			}
			return laws_list;
		}

		public void PrintInLaws()
		{
			List<Person> list = GetInLaws();
			if (CheckThatArrayExist(list))
			{
				foreach (Person laws in list)
				{
					Console.WriteLine($"У {name} In-laws: {laws.name}");
				}
			}
			else
			{
				PrintNothing($"{name} PrintInLaws");
			}
		}
	}
}
