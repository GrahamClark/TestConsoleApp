using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.Grammars
{
    class Runner : IRunner
    {
        const string parenGrammar = @"
            S:           S S | ( PARENEND | [ BRACKETEND | { BRACEEND | < ANGLEEND
            PARENEND:    S ) | NIL )
            BRACKETEND:  S ] | NIL ]
            BRACEEND:    S } | NIL }
            ANGLEEND:    S > | NIL >
            ";

        const string classGrammar = @"
            CLASSLIST:       CLASSDECL CLASSLIST | NIL NIL
            CLASSDECL:       HEADER CLASSBODY
            HEADER:          CLSNAME BASE
            CLSNAME:         PUBCLS CLASSNAME
            PUBCLS:          public class
            CLASSNAME:       ID PARAMSLIST
            PARAMSLIST:      NIL NIL | < PARAMSLISTEND
            PARAMSLISTEND:   PARAMSLISTBODY >
            PARAMSLISTBODY:  ID PARAMSLISTREST
            PARAMSLISTREST:  NIL NIL | , PARAMSLISTBODY
            BASE:            NIL NIL | : TYPE
            TYPE:            NODOTTYPE NIL | TYPEDOT TYPE 
            NODOTTYPE:       ID TYPEARGS
            TYPEDOT:         NODOTTYPE . 
            TYPEARGS:        NIL NIL | < ARGLISTEND
            ARGLISTEND:      ARGLISTBODY > 
            ARGLISTBODY:     TYPE ARGLISTREST
            ARGLISTREST:     NIL NIL | , ARGLISTBODY
            CLASSBODY:       { CLASSBODYEND
            CLASSBODYEND:    CLASSLIST }
            ID:              a NIL | b NIL | c NIL | d NIL 
            ";

        public void RunProgram()
        {
            Grammar g1 = new Grammar(parenGrammar);
            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine("{0}:", i);

                foreach (var s in g1.All("S", i))
                {
                    Console.WriteLine("\t{0}", s);
                }
            }

            Grammar g2 = new Grammar(classGrammar);
            for (int i = 1; i < 20; i++)
            {
                Console.WriteLine("{0}:", i);

                foreach (var s in g2.All("CLASSLIST", i))
                {
                    Console.WriteLine("\t{0}", s);
                }
            }
        }
    }
}
