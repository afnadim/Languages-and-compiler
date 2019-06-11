
using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Parameters;

namespace Triangle.Compiler.SyntacticAnalyzer
{
    public partial class Parser
    {
        ///////////////////////////////////////////////////////////////////////////////
        //
        // EXPRESSIONS
        //
        ///////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Parses the expression, and constructs an AST to represent its phrase
        /// structure.
        /// </summary>
        /// <returns>
        /// an <link>Triangle.SyntaxTrees.Expressions.Expression</link>
        /// </returns> 
        /// <throws type="SyntaxError">
        /// a syntactic error
        /// </throws>
        Expression ParseExpression()
        {
            Compiler.WriteDebuggingInfo("Parsing Expression");
            Location startLocation = tokens.Current.Start;
            Expression expression= ParseSecondaryExpression();
            
            if (tokens.Current.Kind == TokenKind.QuestionMark)
            {
                Compiler.WriteDebuggingInfo("Parsing Ternary Expression");
                AcceptIt();
                Expression expression2 = ParseExpression();
                
                Accept(TokenKind.Colon);
                Expression expression3 = ParseExpression();
                SourcePosition expressionPos = new SourcePosition(startLocation, tokens.Current.Finish);
                return new TernaryExpression(expression, expression2, expression3, expressionPos);
            }
            else
            {
                SourcePosition expressionPos = new SourcePosition(startLocation, tokens.Current.Finish);
                return expression;
            }
        }

        /// <summary>
        // Parses the secondary expression, and constructs an AST to represent its
        /// phrase structure.
        /// </summary>
        /// <returns>
        /// an <link>Triangle.SyntaxTrees.Expressions.Expression</link>
        /// </returns>
        /// <throws type="SyntaxError">
        /// a syntactic error
        /// </throws>
        Expression ParseSecondaryExpression()
        {
            Compiler.WriteDebuggingInfo("Parsing Secondary Expression");
            Location startLocation = tokens.Current.Start;
            Expression expression = ParsePrimaryExpression();
            while (tokens.Current.Kind == TokenKind.Operator)
            {
                
                Operator operator1 = ParseOperator();
                Expression expression2 = ParsePrimaryExpression();
                SourcePosition expressionPos = new SourcePosition(startLocation, tokens.Current.Finish);
                return new BinaryExpression(expression, operator1, expression2, expressionPos);
                
            }
        return expression;
        }

        /// <summary>
        /// Parses the primary expression, and constructs an AST to represent its
        /// phrase structure.
        /// </summary>
        /// <returns>
        /// an <link>Triangle.SyntaxTrees.Expressions.Expression</link>
        /// </returns>
        /// <throws type="SyntaxError">
        /// a syntactic error
        /// </throws>
        Expression ParsePrimaryExpression()
        {
            Compiler.WriteDebuggingInfo("Parsing Primary Expression");
            Location startLocation = tokens.Current.Start;
            switch (tokens.Current.Kind)
            {
                case TokenKind.IntLiteral:
                    {
                        Compiler.WriteDebuggingInfo("Parsing Int Expression");
                        IntegerLiteral integerLiteral= ParseIntegerLiteral();
                        SourcePosition intPos = new SourcePosition(startLocation, tokens.Current.Finish);
                        return new IntegerExpression(integerLiteral, intPos);
                    }

                case TokenKind.CharLiteral:
                    {
                        Compiler.WriteDebuggingInfo("Parsing Char Expression");
                        CharacterLiteral characterLiteral= ParseCharacterLiteral();
                        SourcePosition expressionPos = new SourcePosition(startLocation, tokens.Current.Finish);
                        return new CharacterExpression(characterLiteral, expressionPos);
                    }


                case TokenKind.Identifier:
                    {
                        Compiler.WriteDebuggingInfo("Parsing Call Expression or Identifier Expression");
                        Identifier identifier= ParseIdentifier();
                        if (tokens.Current.Kind == TokenKind.LeftBracket)
                        {
                            Compiler.WriteDebuggingInfo("Parsing Call Expression");
                            AcceptIt();
                            ParameterSequence parameters = ParseParameters();
                            Accept(TokenKind.RightBracket);
                            SourcePosition expressionPos = new SourcePosition(startLocation, tokens.Current.Finish);
                            return new CallExpression(identifier, parameters, expressionPos);
                        }
                        else
                        {
                            Compiler.WriteDebuggingInfo("Parsing Identifier Expression");
                            SourcePosition expressionPos = new SourcePosition(startLocation, tokens.Current.Finish);
                            return new IdExpression(identifier, expressionPos);
                        }
                    }

                case TokenKind.Operator:
                    {
                        Compiler.WriteDebuggingInfo("Parsing Unary Expression");
                        Operator operator1 = ParseOperator();
                        Expression expression = ParsePrimaryExpression();
                        SourcePosition expressionPos = new SourcePosition(startLocation, tokens.Current.Finish);
                        return new UnaryExpression(operator1, expression, expressionPos);
                    }

                case TokenKind.LeftBracket:
                    {
                        Compiler.WriteDebuggingInfo("Parsing Bracket Expression");
                        AcceptIt();
                        Expression expression = ParseExpression();
                        Accept(TokenKind.RightBracket);
                        //return new EmptyExpression();
                        return expression;
                       
                    }

                default:
                    {
                        RaiseSyntacticError("\"%\" cannot start an expression", tokens.Current);
                        return null;
                    }
            }
        }

    }
}