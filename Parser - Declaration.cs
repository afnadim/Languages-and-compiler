/**
 * @Author: Abdullah Ferdous
 * @Date:   27/10/2018
 * @Filename: Parser - Declarations.cs
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Triangle.Compiler.SyntacticAnalyzer
{
    public partial class Parser

    {
        //parseing declaration
        void ParseDeclaration()
        {
            System.Console.WriteLine("Parsing Declaration     " + "          " + "[" + tokens.Current.Spelling + "}" + "       " + tokens.Current.Position.ToString());
            ParseSingleDeclaration();
            while (tokens.Current.Kind == TokenKind.Semicolon)
            {
                AcceptIt();
                ParseSingleDeclaration();
            }
        }


        //parseing  single declaration
        void ParseSingleDeclaration()
        {

            System.Console.WriteLine("Parsing Single Declaration" + "           " + "[" + tokens.Current.Spelling + "}" + "       " + tokens.Current.Position.ToString());
            switch (tokens.Current.Kind)
            {
                case (TokenKind.Const):
                    {
                        AcceptIt();
                        ParseIdentifier();
                        Accept(TokenKind.Is);
                        ParseExpression();
                        break;
                    }

                case (TokenKind.Var):
                    {
                        AcceptIt();
                        ParseIdentifier();
                        Accept(TokenKind.Colon);
                        ParseTypeDenoter();
                        if (tokens.Current.Kind == TokenKind.Becomes)
                        {
                            AcceptIt();
                            ParseExpression();
                        }
                        break;
                    }

                default:
                    Console.WriteLine("Error while parsing Declaration" + "         " + "[" + tokens.Current.Spelling + "}" + "       " + tokens.Current.Position.ToString());
                    //report the error and move to the next token
                    //increment errorCount
                    errorCount++;
                    tokens.MoveNext();  
                    break;

            }

        }

    }
}
