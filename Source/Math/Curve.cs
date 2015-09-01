using System;
using System.Collections.Generic;

namespace PICO8Tool
{
	public class Curve
	{
		public class Key
		{
			public float Time { get; set; }
			public float Value { get; set; }
		}

		public Curve()
		{
			Keys = new List<Key>();
        }

		public List<Key> Keys { get; private set; }

		public void Add(Key key)
		{
			Keys.Add(key);
		}

		public Key Add(float time, float value)
		{
			var key = new Key { Time = time, Value = value };
			Keys.Add(key);
			return key;
		}

		public Key FindNearestKey(float time)
		{
			int index;
			return FindNearestKey(time, out index);
		}

		public Key FindNearestKey(float time, out int index)
		{
			index = -1;

			if (Keys.Count == 0 || time<Keys[0].Time)
			{
				return null;
			}

			int count = Keys.Count;

			if (time > Keys[count - 1].Time)
			{
				index = count - 1;
				return Keys[index];
			}

			for (int i = count - 1; i >= 0; --i)
			{
				if (time >= Keys[i].Time)
				{
					index = i;
					return Keys[i];
				}
			}
			
			return null;
		}

		public bool InBound(float time)
		{
			if (Keys.Count == 0)
			{
				return false;
			}

			return time >= Keys[0].Time && time <= Keys[Keys.Count - 1].Time;
		}

		public float LowerValue
		{
			get { return Keys.Count == 0 ? 0.0f : Keys[0].Value; }
		}

		public float UpperValue
		{
			get { return Keys.Count == 0 ? 0.0f : Keys[Keys.Count - 1].Value; }
		}

		public float LowerTime
		{
			get { return Keys.Count == 0 ? 0.0f : Keys[0].Time; }
		}

		public float UpperTime
		{
			get { return Keys.Count == 0 ? 0.0f : Keys[Keys.Count - 1].Time; }
		}

		public bool LowerBound(float time)
		{
			return Keys.Count == 0 || time < Keys[0].Time;
		}

		public bool UpperBound(float time)
		{
			return Keys.Count == 0 || time > Keys[Keys.Count-1].Time;
		}

		public virtual float Eval(float t)
		{
			return CatRomEval(this, t);
        }

		public static float CatRomEval(Curve curve, float t)
		{
			if (curve == null || curve.Keys.Count == 0)
			{
				return 0.0f;
			}

			int i2 = 0;
			foreach (var a in curve.Keys)
			{
				if (t < a.Time)
				{
					break;
				}

				i2++;
			}

			if (i2 == 0)
			{
				return curve.Keys[0].Value;
			}

			if (i2 == curve.Keys.Count)
			{
				return curve.Keys[curve.Keys.Count - 1].Value;
			}

			int i1 = i2 - 1;

			// Previous point if it exists, otherwise point opposite first from second:
			// Four points are:  prev-first-second-next
			float prevValue, nextValue;

			if (i1 == 0)
			{
				prevValue = 2.0f * curve.Keys[i1].Value + (-1.0f * curve.Keys[i2].Value);
			}
			else
			{
				prevValue = curve.Keys[i1 - 1].Value;
			}

			// Previous point if it exists, otherwise point opposite second from first
			int n = i2 + 1;
			if (n >= curve.Keys.Count - 1)
			{
				nextValue = 2.0f * curve.Keys[i2].Value + (-1.0f * curve.Keys[i1].Value);
			}
			else
			{
				nextValue = curve.Keys[n].Value;
			}

			// interp
			t = (t - curve.Keys[i1].Time) / (curve.Keys[i2].Time - curve.Keys[i1].Time);

			return ((-t + 2.0f) * t - 1.0f) * t / 2.0f * prevValue +
					(((3.0f * t - 5.0f) * t) * t + 2.0f) / 2.0f * curve.Keys[i1].Value +
					((-3.0f * t + 4.0f) * t + 1.0f) * t / 2.0f * curve.Keys[i2].Value +
					((t - 1.0f) * t * t) / 2.0f * nextValue;
		}
	}
}
