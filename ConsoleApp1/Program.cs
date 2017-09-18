using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentConsolePong
{


	public interface Identifyable
	{
		string Name();
	}

	public interface Edible : Identifyable
	{
		/// <summary>
		/// Eats the edible thing
		/// </summary>
		void Eat();

		void ExplainTaste();

		bool IsGoodTasting();
	}

	public class EdibleAdapter : Edible
	{
		public virtual void Eat() { Program.WriteMessage("You eat the " + Name()); }

		public void ExplainTaste()
		{
			Program.WriteMessageIfElse("The " + Name() + " is", " very good tasting.", "", " not", IsGoodTasting());
		}

		public virtual string Name()
		{
			return "Mysterious Food";
		}

		public virtual bool IsGoodTasting()
		{
			return true;
		}
	}
	public class Taco : EdibleAdapter
	{
		public override string Name()
		{
			return "Taco";
		}
	}
	public class Chicken : EdibleAdapter
	{
		public override string Name()
		{
			return "Chicken";
		}
	}
	public class RottenMeat : EdibleAdapter
	{
		public override void Eat()
		{
			Program.WriteMessage("You precautiously eat the " + Name());
		}
		public override bool IsGoodTasting()
		{
			return false;
		}
	}


	public class Calculator
	{

		public abstract class Operator
		{
			public class OperatorFactory
			{
				Dictionary<OperatorKind, Operator> book = new Dictionary<OperatorKind, Operator>();

				public OperatorFactory()
				{
					book.Add(OperatorKind.ADD, new Add());
					book.Add(OperatorKind.SUB, new Sub());
					book.Add(OperatorKind.DIV, new Div());
					book.Add(OperatorKind.MUL, new Mul());

				}

				public Operator create(char c)
				{
					OperatorKind kind = (OperatorKind)c;

					return book[kind];
				}
					
			}

			public enum OperatorKind
			{
				NULL,
				ADD = '+',
				SUB = '-',
				DIV = '/',
				MUL = '*',
				NUM
			}


			public OperatorKind kind;

			protected Operator(OperatorKind kind)
			{
				this.kind = kind;
			}

			private class Num : Operator
			{
				public Num() : base(OperatorKind.NUM) { }
				protected override float execute(float l, float r = 0)
				{
					return l;
				}
			}
			private class Add : Operator
			{
				public Add() : base(OperatorKind.ADD) { }
				protected override float execute(float l, float r)
				{
					return l + r;
				}
			}
			private class Sub : Operator
			{
				public Sub() : base(OperatorKind.SUB) { }
				protected override float execute(float l, float r)
				{
					return l - r;
				}
			}
			private class Mul : Operator
			{
				public Mul() : base(OperatorKind.MUL) { }

				protected override float execute(float l, float r)
				{
					return l * r;
				}
			}

			private class Div : Operator
			{
				public Div() : base(OperatorKind.DIV) { }

				protected override float execute(float l, float r)
				{
					if (r == 0)
					{
						Program.WriteMessage("Divide by Zero");
						return 0;
					}
					return l / r;
				}
			}

			protected abstract float execute(float l, float r);
			//public void execute(
		}

		public class Operation
		{
			string data;


		}
		public void execute(string text)
		{
			//1 split data so that the parenthies are out
		}
	}

   
    //A 





   
   

    public static class F
    {
        public static char Set(out char l, char r)
        {
            return l = r;
        }
        public static string Add(string a, char b)
        {
            return Add(a, b.ToString());
        }
        public static string Add(char a, string b)
        {
            return Add(a.ToString(), b);
        }
        public static string Add(string a, string b)
        {
            return a + b;
        }
        public static int Subtract(int l, int r)
        {
            return l - r;
        }
        public static bool Greater(int l, int r)
        {
            return l < r;
        }
        public static bool Or(bool a, params bool[] b)
        {
            return a || b.Contains(true);
        }
        public static bool Negate(bool value)
        {
            return !value;
        }
    }

    public static class CharHelper
    {
        public static readonly char[] YES_CHAR = { 'Y', 'y' };
        public static readonly char[] NO_CHAR = { 'N', 'n' };
        
        public static bool IsLetter(char c)
        {
            return char.IsLetter(c);
        }
        public static bool IsDigit(char c)
        {
            return char.IsDigit(c);
        }
        public static bool IsNo(char c)
        {
            return NO_CHAR.Contains(c);
        }
        public static int ToInt(char c)
        {
            return F.Subtract(c, '0');
        }
        public static bool IsGreaterThan(char c, int amount)
        {
            return F.Greater(ToInt(c), amount);
        }
        public static bool IsYesOrNo(char c)
        {
            return F.Or(IsYes(c), IsNo(c));
        }
        public static bool IsYes(char c)
        {
            return YES_CHAR.Contains(c);
        }
        public static bool IsAny(char input, params char[] list)
        {
            return list.Contains(input);
        }
    }

    class Program
    {



        public static void WriteMessage(string message)
        {
            Console.WriteLine(F.Add('\n', message));
        }
        public static ConsoleKeyInfo GetKey()
        {
            return Console.ReadKey();
        }

        public static char GetInput(string incorrectMessage, Func<char, bool> trueFunction)
        {
            char c;
            while (F.Negate(trueFunction(F.Set(out c, GetKey().KeyChar))))
            {
                WriteMessage(incorrectMessage);
            }
            return c;
        }
        public static char GetCharacter(string incorrectMessage = "Please enter a character.")
        {
            return GetInput(incorrectMessage, CharHelper.IsLetter);
        }
        public static char GetDigit(string incorrectMessage = "Please enter a digit.")
        {
            return GetInput(incorrectMessage, CharHelper.IsDigit);
        }
        public static char GetYesNo(string incorrectMessage = "Please enter y or n.")
        {
            return GetInput(incorrectMessage, CharHelper.IsYesOrNo);
        }
        //----
        public static bool WriteMessageIf(string message, bool value)
        {
            // Like he said, state logic is the only time you cannot get rid of if statemeuts
            if(value)
            {
                WriteMessage(message);
            }
            return value;
        }
        public static bool WriteMessageIfElse(string trueMessage, string falseMessage, bool value)
        {
            return  F.Negate(WriteMessageIf(falseMessage, F.Negate(WriteMessageIf(trueMessage, value))));
		}
		public static bool WriteMessageIfElse(string messagePrefix, string messagePostfix, string trueMessage, string falseMessage, bool value)
		{
			string fullTrueMessage = F.Add(F.Add(messagePrefix, trueMessage), messagePostfix);
			string fullFalseMessage = F.Add(F.Add(messagePrefix, falseMessage), messagePostfix);
			return WriteMessageIfElse(fullTrueMessage, fullFalseMessage, value);
		}

		public static bool AskNumberQuestion(string message, string higherResponse, string lowerOrEqualResponse, int lowerAmount)
        {
            WriteMessage(message);
            return WriteMessageIfElse(higherResponse, lowerOrEqualResponse, CharHelper.IsGreaterThan(GetDigit(), lowerAmount));
        }

        public static bool AskYNQuestion(string message, string yesMessage, string noMessage)
        {
            WriteMessage(message);
            return WriteMessageIfElse(yesMessage, noMessage, CharHelper.IsYes(GetYesNo()));
        }

		[STAThread] //required for input helper
		static void Main(string[] args)
        {
			InputHelper.Init();
			Pong pong = new Pong();

			while (true)
			{
				InputHelper.Update();
				pong.Update(TimeHelper.TIME_SCALE);

				System.Threading.Thread.Sleep(20);

				pong.Draw();
				pong.Write();
			}
			List<EdibleAdapter> foods = new List<EdibleAdapter>();
			foods.Add(new Chicken());
			foods.Add(new Taco());
			foods.Add(new RottenMeat());

			foreach (EdibleAdapter food in foods)
			{
				food.Eat();
				food.ExplainTaste();
			}

            while (true)
            {


                if (AskYNQuestion("Are you older than 13?", "Good! You do not need your parents permission to use this application!",
                    "Well then, I guess you cannot continue with this. Sorry."))
                {
                    if (AskYNQuestion("Do you like Apples?", "They are sweet.", "Oh..."))
                    {
                        if (AskNumberQuestion("How many Apples have you had recently",
                           "Nice, that seems like a healthy amount.", "That is a lot of fiber! I worry for you.", 5))
                        {
                            break;
                        }
                    } 
                    else
                    {
                        AskYNQuestion("What about Oranges? Do you like them?",
                            "Well Orange you glad I met you then!",
                            "Dang, I guess you do not like a lot or fruit then");
                    }
                }
            }
            //Keep the console window olen in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
