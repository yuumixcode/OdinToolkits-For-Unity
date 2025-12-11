using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Examples;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    [Searchable]
    public class SearchableExample : ExampleSO
    {
        #region ExampleEnum enum

        public enum ExampleEnum
        {
            One,
            Two,
            Three,
            Four,
            Five
        }

        #endregion

        #region Nested type: ${0}

        [Serializable]
        public struct ExampleStruct
        {
            public string Name;
            public int Number;
            public ExampleEnum Enum;

            public ExampleStruct(int nr) : this()
            {
                Name = "Element " + nr;
                Number = nr;
                Enum = (ExampleEnum)ExampleHelper.RandomInt(0, 5);
            }
        }

        #endregion

        #region Searchable 普通参数

        [TabGroup("SearchableGroup", "Searchable 普通参数", TextColor = "lightpurple")]
        [Title("默认不特殊设置参数", "默认开启模糊搜索，开启递归搜索，筛选条件为 All")]
        [Searchable]
        public List<Perk> perks = new List<Perk>
        {
            new Perk
            {
                Name = "Old Sage",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Wisdom, Value = 2 },
                    new Effect { Skill = Skill.Intelligence, Value = 1 },
                    new Effect { Skill = Skill.Strength, Value = -2 }
                }
            },
            new Perk
            {
                Name = "Hardened Criminal",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Dexterity, Value = 2 },
                    new Effect { Skill = Skill.Strength, Value = 1 },
                    new Effect { Skill = Skill.Charisma, Value = -2 }
                }
            },
            new Perk
            {
                Name = "Born Leader",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Charisma, Value = 2 },
                    new Effect { Skill = Skill.Intelligence, Value = -3 }
                }
            },
            new Perk
            {
                Name = "Village Idiot",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Charisma, Value = 4 },
                    new Effect { Skill = Skill.Constitution, Value = 2 },
                    new Effect { Skill = Skill.Intelligence, Value = -3 },
                    new Effect { Skill = Skill.Wisdom, Value = -3 }
                }
            }
        };

        [TabGroup("SearchableGroup", "Searchable 普通参数")]
        [Title("FuzzySearch 参数，关闭模糊搜索，大小写必须准确", "Old Sage 可以成功匹配，但是 old 无法匹配")]
        [Searchable(FuzzySearch = false)]
        public List<Perk> perks2 = new List<Perk>
        {
            new Perk
            {
                Name = "Old Sage",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Wisdom, Value = 2 },
                    new Effect { Skill = Skill.Intelligence, Value = 1 }
                }
            }
        };

        [TabGroup("SearchableGroup", "Searchable 普通参数")]
        [Title("Recursive 参数，取消递归搜索", "可以搜索 Perk 类中的字段 Name -> Old Sage，但是无法递归搜索 Effects 中的 Wisdom")]
        [Searchable(Recursive = false)]
        public List<Perk> perks3 = new List<Perk>
        {
            new Perk
            {
                Name = "Old Sage",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Wisdom, Value = 2 }
                }
            }
        };

        [Serializable]
        public class Perk
        {
            #region Serialized Fields

            public string Name;

            [TableList]
            public List<Effect> effects;

            #endregion
        }

        [Serializable]
        public class Effect
        {
            #region Serialized Fields

            public Skill Skill;
            public float Value;

            #endregion
        }

        public enum Skill
        {
            Strength,
            Dexterity,
            Constitution,
            Intelligence,
            Wisdom,
            Charisma
        }

        #endregion

        #region Searchable 枚举参数

        [TabGroup("SearchableGroup", "Searchable 枚举参数", TextColor = "orange")]
        [Title("FilterOptions = SearchFilterOptions.PropertyName",
            "筛选 Name，effects 这样的字段名，通常用于类搜索字段，列表中的元素都是一样的")]
        [Searchable(FilterOptions = SearchFilterOptions.PropertyName)]
        public List<Perk> perks5 = new List<Perk>
        {
            new Perk
            {
                Name = "Old Sage",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Wisdom, Value = 2 },
                    new Effect { Skill = Skill.Intelligence, Value = 1 }
                }
            },
            new Perk
            {
                Name = "Hardened Criminal",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Dexterity, Value = 2 },
                    new Effect { Skill = Skill.Charisma, Value = -2 }
                }
            },
            new Perk
            {
                Name = "Born Leader",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Charisma, Value = 2 },
                    new Effect { Skill = Skill.Intelligence, Value = -3 }
                }
            }
        };

        [TabGroup("SearchableGroup", "Searchable 枚举参数")]
        [Title("FilterOptions = SearchFilterOptions.PropertyNiceName",
            "筛选 Name，Effects 的 NiceName 字段名，通常用于类搜索字段，列表中的元素都是一样的")]
        [Searchable(FilterOptions = SearchFilterOptions.PropertyNiceName)]
        public List<Perk> perks6 = new List<Perk>
        {
            new Perk
            {
                Name = "Old Sage",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Wisdom, Value = 2 },
                    new Effect { Skill = Skill.Intelligence, Value = 1 }
                }
            },
            new Perk
            {
                Name = "Hardened Criminal",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Dexterity, Value = 2 },
                    new Effect { Skill = Skill.Charisma, Value = -2 }
                }
            },
            new Perk
            {
                Name = "Born Leader",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Charisma, Value = 2 },
                    new Effect { Skill = Skill.Intelligence, Value = -3 }
                }
            }
        };

        [TabGroup("SearchableGroup", "Searchable 枚举参数")]
        [Title("FilterOptions = SearchFilterOptions.TypeOfValue", "匹配元素值的类型，比如 Name 字段类型为 string")]
        [Searchable(FilterOptions = SearchFilterOptions.TypeOfValue)]
        public List<Perk> perks7 = new List<Perk>
        {
            new Perk
            {
                Name = "Old Sage",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Wisdom, Value = 2 },
                    new Effect { Skill = Skill.Intelligence, Value = 1 }
                }
            },
            new Perk
            {
                Name = "Hardened Criminal",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Dexterity, Value = 2 },
                    new Effect { Skill = Skill.Charisma, Value = -2 }
                }
            },
            new Perk
            {
                Name = "Born Leader",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Charisma, Value = 2 },
                    new Effect { Skill = Skill.Intelligence, Value = -3 }
                }
            }
        };

        [TabGroup("SearchableGroup", "Searchable 枚举参数")]
        [Title("FilterOptions = SearchFilterOptions.ValueToString", "匹配元素值执行 ToString 的结果，通常就是显示在面板上的值")]
        [Searchable(FilterOptions = SearchFilterOptions.ValueToString)]
        public List<Perk> perks8 = new List<Perk>
        {
            new Perk
            {
                Name = "Old Sage",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Wisdom, Value = 2 },
                    new Effect { Skill = Skill.Intelligence, Value = 1 }
                }
            },
            new Perk
            {
                Name = "Hardened Criminal",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Dexterity, Value = 2 },
                    new Effect { Skill = Skill.Charisma, Value = -2 }
                }
            },
            new Perk
            {
                Name = "Born Leader",
                effects = new List<Effect>
                {
                    new Effect { Skill = Skill.Charisma, Value = 2 },
                    new Effect { Skill = Skill.Intelligence, Value = -3 }
                }
            }
        };

        [TabGroup("SearchableGroup", "Searchable 枚举参数")]
        [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        [Title("FilterOptions = SearchFilterOptions.ISearchFilterableInterface",
            "列表元素实现 ISearchFilterable 接口，自定义筛选条件，匹配 Square 平方值")]
        public List<FilterableBySquareStruct> customFiltering = new List<FilterableBySquareStruct>(Enumerable
            .Range(1, 10)
            .Select(i => new FilterableBySquareStruct(i)));

        #endregion

        #region Searchable 扩展

        [TabGroup("SearchableGroup", "Searchable 扩展", TextColor = "green")]
        [Searchable]
        [Title("自定义类使用 [Searchable]")]
        [InfoBox("作用等同于对 SO 或者 MonoBehaviour 脚本，也可以直接在声明类的地方添加 [Searchable]，那么就不需要每次都添加")]
        public ExampleClass searchableClass = new ExampleClass();

        [Serializable]
        public class ExampleClass
        {
            #region Serialized Fields

            public string someString = "Saehrimnir is a tasty delicacy";
            public int someInt = 13579;

            public DataContainer dataContainerOne = new DataContainer { Name = "Example Data Set One" };

            #endregion
        }

        [Serializable]
        [Searchable]
        public class DataContainer
        {
            #region Serialized Fields

            public string Name;

            public List<ExampleStruct> data = new List<ExampleStruct>(Enumerable.Range(1, 10)
                .Select(i => new ExampleStruct(i)));

            #endregion
        }

        [Serializable]
        public struct FilterableBySquareStruct : ISearchFilterable
        {
            public int number;

            [ShowInInspector]
            [DisplayAsString]
            [EnableGUI]
            public int Square => number * number;

            public FilterableBySquareStruct(int nr) => number = nr;

            public bool IsMatch(string searchString) => searchString.Contains(Square.ToString());
        }

        #endregion
    }
}
