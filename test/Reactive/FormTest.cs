
using Xunit;
using Skclusive.Mobx.JsonSchema;
using Skclusive.Core.Collection;

namespace Skclusive.Mobx.Form.Tests
{
    public class FormTest
    {
        [Fact]
        public void TestCreate()
        {
            var form = AppTypes.FormType.Create(new Form
            {
                Sections = new Section[]
                {
                    new Section
                    {
                        Title = "Basic",

                        Selected = true,

                        Outline =  new Outline
                        (
                            "name",

                            "birthdate",

                            new Outline []
                            {
                                "size",

                                "color"
                            }
                        )
                    },

                    new Section
                    {
                        Title = "Others",

                        Outline = new Outline
                        (
                            "agree",

                            "array"
                        )
                    },
                },

                Schema = new Object
                {
                    Type = SchemaType.Object,

                    Title = "nothing?",

                    Properties = new Map<string, IAny>
                    {
                        ["agree"] = new Boolean
                        {
                            Type = SchemaType.Boolean,

                            Const = true,

                            Enum = new bool[] { true, false },

                            Value = true,

                            Title = "agree?"
                        },
                        ["textual"] = new String
                        {
                            Type = SchemaType.String,

                            Const = "const",

                            Enum = new string[] { "one", "two", "three" },

                            Value = "six",

                            Title = "textual?",

                            MinLength = 1,

                            MaxLength = 8,

                            Pattern = "pattern",

                            Format = Format.DateTime
                        },
                        ["sequence"] = new Number
                        {
                            Type = SchemaType.Number,

                            Const = 10,

                            Enum = new double?[] { 1, 3, 5 },

                            Value = 6,

                            Title = "sequence?",

                            Minimum = 1,

                            Maximum = 8,

                            MultipleOf = 2
                        },
                        ["complex"] = new Object
                        {
                            Type = SchemaType.Object,

                            Title = "complex?",

                            Properties = new Map<string, IAny>
                            {
                                ["crazy"] = new Boolean
                                {
                                    Type = SchemaType.Boolean,

                                    Const = true,

                                    Enum = new bool[] { true, false },

                                    Value = true,

                                    Title = "crazy?"
                                }
                            }
                        }
                    }
                }
            });

            Assert.NotNull(form);

            form.Validate();

            Assert.False(form.Valid);

            var fieldError = form.Error;

            Assert.NotNull(fieldError);
        }
    }
}
