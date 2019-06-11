/**
 * @Author: Abdullah Ferdous
 * @Date:   27/10/2018
 * @Filename: Parser - TypeDenoter.cs
 */


using System;
using System.Collections.Generic;
using System.Text;

namespace Triangle.Compiler.SyntacticAnalyzer
{
    public partial class Parser
    {

        // /////////////////////////////////////////////////////////////////////////////
        //
        // VALUE-OR-VARIABLE NAMES
        //
        // /////////////////////////////////////////////////////////////////////////////


        void ParseTypeDenoter()
        {
            System.Console.WriteLine("parsing type Denoter" + "   " + "[" + tokens.Current.Spelling + "}" + "       " + tokens.Current.Position.ToString());
            ParseIdentifier();

        }

    }
}
