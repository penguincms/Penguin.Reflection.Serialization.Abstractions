using Penguin.Reflection.Serialization.Abstractions.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Penguin.Reflection.Serialization.Abstractions.Wrappers
{
    /// <summary>
    /// Used to generate a unique key for a set of objects, for avoiding duplication during serialization
    /// </summary>
    public class KeyGroup
    {
        #region Properties

        /// <summary>
        /// The objects in the group
        /// </summary>
        public HashSet<object> Objects { get; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new instance of the group
        /// </summary>
        /// <param name="objects"></param>
        public KeyGroup(params object[] objects)
        {
            this.Objects = new HashSet<object>(objects);
        }

        /// <summary>
        /// Creates a new keygroup from an object constructor
        /// </summary>
        /// <param name="oc">The object constructor to wrap</param>
        public KeyGroup(ObjectConstructor oc) : this(oc?.Object, oc?.PropertyInfo, oc?.Type)
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Tests for inequality between keygroups
        /// </summary>
        /// <param name="obj1">The first keygroup</param>
        /// <param name="obj2">the second keygroup</param>
        /// <returns>whether or not the groups are equal</returns>
        public static bool operator !=(KeyGroup obj1, KeyGroup obj2)
        {
            return !(obj1 == obj2);
        }

        /// <summary>
        /// Tests for equality between keygroups
        /// </summary>
        /// <param name="obj1">The first keygroup</param>
        /// <param name="obj2">the second keygroup</param>
        /// <returns>whether or not the groups are equal</returns>
        public static bool operator ==(KeyGroup obj1, KeyGroup obj2)
        {
            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }

            if (obj1 is null)
            {
                return false;
            }

            if (obj2 is null)
            {
                return false;
            }

            HashSet<object> toCheck1 = new HashSet<object>(obj1.Objects);
            HashSet<object> toCheck2 = new HashSet<object>(obj2.Objects);

            foreach (object o in toCheck1)
            {
                if (!toCheck2.Contains(o))
                {
                    return false;
                }
                else
                {
                    _ = toCheck2.Remove(o);
                }
            }

            return !toCheck2.Any();
        }

        /// <summary>
        /// Checks if this keygroup is equal to another
        /// </summary>
        /// <param name="other">the other keygroup</param>
        /// <returns>Whether or not the keygroups are equal</returns>
        public bool Equals(KeyGroup other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            HashSet<object> toCheck1 = new HashSet<object>(this.Objects);
            HashSet<object> toCheck2 = new HashSet<object>(other.Objects);

            foreach (object o in toCheck1)
            {
                if (!toCheck2.Contains(o))
                {
                    return false;
                }
                else
                {
                    _ = toCheck2.Remove(o);
                }
            }

            return !toCheck2.Any();
        }

        /// <summary>
        /// Checks if this keygroup is equal to another
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>If this keygroup is equal to the other</returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (!(obj is KeyGroup))
            {
                return false;
            }

            HashSet<object> toCheck1 = new HashSet<object>(this.Objects);
            HashSet<object> toCheck2 = new HashSet<object>((obj as KeyGroup).Objects);

            foreach (object o in toCheck1)
            {
                if (!toCheck2.Contains(o))
                {
                    return false;
                }
                else
                {
                    _ = toCheck2.Remove(o);
                }
            }

            return !toCheck2.Any();
        }

        /// <summary>
        /// Gets the average hashcode of the objects in the group
        /// </summary>
        /// <returns>The average hashcode of the objects in the group</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return Convert.ToInt32(this.Objects.Average(o => o?.GetHashCode() ?? 0));
            }
        }

        #endregion Methods
    }
}