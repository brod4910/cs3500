﻿// Skeleton implementation written by Joe Zachary for CS 3500, January 2017.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Dependencies
{
    /// <summary>
    /// A DependencyGraph can be modeled as a set of dependencies, where a dependency is an ordered 
    /// pair of strings.  Two dependencies (s1,t1) and (s2,t2) are considered equal if and only if 
    /// s1 equals s2 and t1 equals t2.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that the dependency (s,t) is in DG 
    ///    is called the dependents of s, which we will denote as dependents(s).
    ///        
    ///    (2) If t is a string, the set of all strings s such that the dependency (s,t) is in DG 
    ///    is called the dependees of t, which we will denote as dependees(t).
    ///    
    /// The notations dependents(s) and dependees(s) are used in the specification of the methods of this class.
    ///
    /// For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    ///     dependents("a") = {"b", "c"}
    ///     dependents("b") = {"d"}
    ///     dependents("c") = {}
    ///     dependents("d") = {"d"}
    ///     dependees("a") = {}
    ///     dependees("b") = {"a"}
    ///     dependees("c") = {"a"}
    ///     dependees("d") = {"b", "d"}
    ///     
    /// All of the methods below require their string parameters to be non-null.  This means that 
    /// the behavior of the method is undefined when a string parameter is null.  
    ///
    /// IMPORTANT IMPLEMENTATION NOTE
    /// 
    /// The simplest way to describe a DependencyGraph and its methods is as a set of dependencies, 
    /// as discussed above.
    /// 
    /// However, physically representing a DependencyGraph as, say, a set of ordered pairs will not
    /// yield an acceptably efficient representation.  DO NOT USE SUCH A REPRESENTATION.
    /// 
    /// You'll need to be more clever than that.  Design a representation that is both easy to work
    /// with as well acceptably efficient according to the guidelines in the PS3 writeup. Some of
    /// the test cases with which you will be graded will create massive DependencyGraphs.  If you
    /// build an inefficient DependencyGraph this week, you will be regretting it for the next month.
    /// </summary>
    /// 


    public class DependencyGraph
    {
        private Dictionary<String, List<String>> Dependents;
        private Dictionary<String, List<String>> Dependees;
        private int size;

        /// <summary>
        /// Creates a DependencyGraph containing no dependencies.
        /// </summary>
        public DependencyGraph()
        {
            Dictionary<String, List<String>> Dependents = new Dictionary<String, List<String>>();
            Dictionary<String, List<String>> Dependees = new Dictionary<String, List<String>>();

            this.Dependents = Dependents;
            this.Dependees = Dependees;
        }

        /// <summary>
        /// The number of dependencies in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return size; }
        }

        /// <summary>
        /// Reports whether dependents(s) is non-empty.  Requires s != null.
        /// </summary>
        public bool HasDependents(string s)
        {
            List<String> dependentList = new List<string>();

            if (s != null)
            {
                return Dependents.TryGetValue(s, out dependentList);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Reports whether dependees(s) is non-empty.  Requires s != null.
        /// </summary>
        public bool HasDependees(string s)
        {
            List<String> dependeeList = new List<string>();

            if (s != null)
            {
                return Dependees.TryGetValue(s, out dependeeList);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Enumerates dependents(s).  Requires s != null.
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            List<String> dependentList = new List<string>();

            if(s != null)
            {
                if(Dependents.ContainsKey(s))
                {
                   Dependents.TryGetValue(s, out dependentList);

                    foreach(string dependent in dependentList)
                    {
                        yield return dependent;
                    }
                }
            }
        }

        /// <summary>
        /// Enumerates dependees(s).  Requires s != null.
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            List<String> dependeeList = new List<string>();

            if (s != null)
            {
                if (Dependees.ContainsKey(s))
                {
                    Dependents.TryGetValue(s, out dependeeList);

                    foreach (string dependee in dependeeList)
                    {
                        yield return dependee;
                    }
                }
            }
        }

        /// <summary>
        /// Adds the dependency (s,t) to this DependencyGraph.
        /// This has no effect if (s,t) already belongs to this DependencyGraph.
        /// Requires s != null and t != null.
        /// (dependee, dependent)
        /// </summary>
        public void AddDependency(string s, string t)
        {
            if(s != null && t != null)
            {
                List<String> dependentList = new List<string>();
                List<String> dependeeList = new List<string>();

                if (!Dependents.ContainsKey(s) || !Dependees.ContainsKey(t))
                {
                    if (!Dependents.ContainsKey(s))
                    {
                        dependentList.Add(t);
                        Dependents.Add(s, dependentList);
                        size++;
                    }
                    else if(Dependents.ContainsKey(s))
                    {
                        Dependents.TryGetValue(s, out dependentList);

                        if(!dependentList.Contains(t))
                        {
                            dependentList.Add(t);
                            Dependents.Remove(s);
                            Dependents.Add(s, dependentList);
                            size++;
                        }
                    }

                    if(!Dependees.ContainsKey(t))
                    {
                        dependeeList.Add(s);
                        Dependees.Add(t, dependeeList);
                        size++;
                    }
                    else if(Dependees.ContainsKey(t))
                    {
                        Dependees.TryGetValue(s, out dependeeList);

                        if (!dependeeList.Contains(t))
                        {
                            dependeeList.Add(s);
                            Dependees.Remove(t);
                            Dependees.Add(t, dependeeList);
                            size++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes the dependency (s,t) from this DependencyGraph.
        /// Does nothing if (s,t) doesn't belong to this DependencyGraph.
        /// Requires s != null and t != null.
        /// </summary>
        public void RemoveDependency(string s, string t)
        {
            if (s != null && t != null)
            {
                List<String> dependentList = new List<string>();
                List<String> dependeeList = new List<string>();

                if(Dependents.ContainsKey(s) || Dependees.ContainsKey(t))
                {
                    if(Dependents.ContainsKey(s))
                    {
                        Dependents.TryGetValue(s, out dependentList);

                        if (dependentList.Contains(t))
                        {
                            dependentList.Remove(t);
                            Dependents.Remove(s);
                            Dependents.Add(s, dependentList);
                        }
                    }

                    if(Dependees.ContainsKey(t))
                    {
                        Dependees.TryGetValue(t, out dependeeList);

                        if (dependeeList.Contains(s))
                        {
                            dependeeList.Remove(s);
                            Dependees.Remove(t);
                            Dependees.Add(t, dependeeList);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes all existing dependencies of the form (s,r).  Then, for each
        /// t in newDependents, adds the dependency (s,t).
        /// Requires s != null and t != null.
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {

        }

        /// <summary>
        /// Removes all existing dependencies of the form (r,t).  Then, for each 
        /// s in newDependees, adds the dependency (s,t).
        /// Requires s != null and t != null.
        /// </summary>
        public void ReplaceDependees(string t, IEnumerable<string> newDependees)
        {
        }
    }
}