using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
// ReSharper disable ConvertToAutoPropertyWhenPossible

#pragma warning disable CS0067 // 事件从未使用过

namespace Yuumix.OdinToolkits.Core.Editor
{
    #region 整体风格样式说明

    // UNITY C# STYLE GUIDE:
    // | The '// |'-style comments denote documentation markup.
    // | This is an example Style Guide to use with your Unity project, inspired by Microsoft's.
    // | Microsoft's Framework Design Guidelines are here: https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/
    // | Google also maintains a style guide here: https://google.github.io/styleguide/csharp-style.html
    // | This guide is like any other style guide, not a rulebook. 
    // | It’s a starting point for your team to discuss and agree on a common style.
    // | There is no one-size-fits-all style guide. Pick and choose what works best for your team and project.
    // | While there is no right solution this guide provides some inspiration on what guidelines you might want to consider.
    // | The most important thing is to be consistent. If you have a team, agree on a style guide and stick to it.
    // | The goal of a style guide is to take the guesswork out of coding conventions 
    // | and formatting so you and your team can focus on the solution.
    // | So with that out of the way, use this guide, MSFT's or Google's as inspiration 
    // | or even as a starting point to create your own style guide.
    // | Omit, add or customize these rules to build your own team style guide, then apply consistently.
    // ---
    // KEY PRINCIPALS
    // | Favor readability over brevity. Clarity is more important than any time saved from omitting a few vowels.
    // | The goal is to make your code more readable, maintainable, and consistent.
    // | Doing things like naming right from the beginning will save you time and effort later. 
    // | Particularly when debugging, and extending functionality.
    // | As much as possible stick to industry standards and conventions. However, be pragmatic. 
    // | The needs are different in 100+ person teams than in a 2-person team.
    // | Building on above, a code style standard is a living document. 
    // | It should be updated as the project evolves and the team grows.
    // | As a beginner, it’s important to learn the rules before you break them. 
    // | As you gain experience, you can make informed decisions about when to deviate.
    // | However, as a beginner, keep your guidelines light. 
    // | The most important thing is to write code that works before making it "clean". 
    // | Code style is a personal preference. The most important thing is to be consistent. 
    // | If you have a team, agree on a style guide and stick to it.
    // | Favor what can be pronounced naturally and readability 
    // | e.g. HorizontalAlignment instead of AlignmentHorizontal (more English-readable)
    // ---
    // NAMING
    // | This one deserves a repeat: Favor readability over brevity. 
    // | Clarity is more important than any time saved from omitting a few vowels.
    // | Pick meaningful names from the beginning to minimize refactoring later.
    // | Variable names should be descriptive, clear, and unambiguous because they represent a thing or state.
    // | Use a noun when naming them except when the variable is of the type bool.
    // | Prefix Booleans with a verb to make their meaning more apparent. e.g. isDead, isWalking, hasDamageMultiplier.
    // | Use meaningful names. Don’t abbreviate (unless it’s math or commonly accepted). Your variable names should reveal their intent. 
    // | Choose identifier names that are easily readable. 
    // | For example, a property named HorizontalAlignment is more readable than AlignmentHorizontal.
    // | Make type names unambiguous across namespaces and problem domains by avoiding common terms 
    // | or adding a prefix or a suffix. (ex. use 'PhysicsSolver', not 'Solver')
    // ---
    // CASING AND PREFIXES:
    // | Use Pascal case (e.g. ExamplePlayerController, MaxHealth, etc.) unless noted otherwise.
    // | Use camel case (e.g. examplePlayerController, maxHealth, etc.) for local/private variables and parameters.
    // | Avoid snake case, kebab case, Hungarian notation.
    // | If you have a MonoBehaviour in a file, the source file name must match.
    // | Consider using se prefixes for private member variables (m_), constants (k_), or static variables (s_), 
    // | so the name can reveal more about the variable at a glance.
    // | Alternatively, some guides suggest adding a prefix to private member variables
    // | with an underscore (_) to differentiate them from local variables.
    // | Drop redundant initializers (i.e. no ‘= 0’ on the ints, ‘= null’ on ref types, etc.) 
    // | as they are initialized to 0 or null by default,
    // | Specify (or omit) access level modifiers consistently. 
    // | We recommended explicitly specifying private to make the access level clear and to avoid any ambiguity.
    // | Avoid redundant names: If your class is called Player, you don’t need to create member variables called PlayerScore or PlayerTarget. 
    // | Trim them down to Score or Target.
    // ---
    // FORMATTING:
    // | Choose K&R (opening curly braces on same line) or Allman (opening curly braces on a new line) style braces.
    // | Readability is key. Try to keep lines short. Consider horizontal whitespace. 
    // | You can also define a standard max line width in your style guide (some prefer less than 120 characters). 
    // | Break a long line into smaller statements rather than letting it overflow.
    // | Use a single space before flow control conditions, e.g. while (x == y).
    // | Avoid spaces inside brackets, e.g. x = dataArray[index].
    // --- 
    // SPACING:
    // | Use a single space after a comma between function arguments, e.g. CollectItem(myObject, 0, 1);
    // | Don’t add a space after the parenthesis and function arguments, e.g. CollectItem(myObject, 0, 1);
    // | Don’t use spaces between a function name and parenthesis, e.g. DropPowerUp(myPrefab, 0, 1);
    // | Use vertical spacing (extra blank line) for visual separation, e.g. for (int i = 0; i < 100; i++) { DoSomething(i); }
    // | Use one variable declaration per line in most cases. It’s less compact, but enhances readability.
    // | Don’t use spaces between a function name and parenthesis, e.g. DropPowerUp(myPrefab, 0, 1);
    // | Use a single space before flow control conditions and a single space before and after comparison operators, e.g. if (x == y).
    // ---
    // COMMENTS:
    // | Comment when needed. That is when the code isn’t self-explanatory and needs clarification beyond good naming revealing the intent.
    // | However, if you need to add a comment to explain a convoluted tangle of logic, consider restructuring your code to be more obvious. 
    // | Good naming can take out the guesswork. Consider renaming before commenting.  
    // | Rather than simply answering "what" or "how," comments can fill in the gaps and tell us "why."
    // | Use the // comment to keep the explanation next to the logic.
    // | Use a Tooltip instead of a comment for serialized fields if your fields in the Inspector need explanation.
    // | Rather than using Regions think of them as a code smell indicating your class is too large and needs refactoring. 
    // | Use a link to an external reference for legal information or licensing to save space.
    // | Use a summary XML tag in front of public methods or functions for output documentation/Intellisense.
    // | Avoid attributions, e.g. // Created by, // Modified by, etc. Use version control to track changes.
    // | Documenting the 'why' is far more important than the 'what' or 'how'.
    // ---
    // CLASS ORGANIZATION:
    // | Organize your class in the following order: Fields, Properties, Events, 
    // | MonoBehaviour methods (Awake, Start, OnEnable, OnDisable, OnDestroy, etc.), public methods, private, methods, other Classes.
    // | Use of " #region " is generally discouraged as it can hide complexity and make it harder to read the code.
    // ---
    // USING LINES:
    // | Keep using lines at the top of your file.
    // | Remove unused lines.
    // ---
    // NAMESPACES:
    // | Use namespaces to ensure that your classes, interfaces, enums, etc. 
    // | won’t conflict with existing ones from other namespaces or the global namespace.
    // | Use Pascal case, without special symbols or underscores.
    // | Add using line at the top to avoid typing namespace repeatedly.
    // | Create sub-namespaces with the dot (.) operator, e.g. MyApplication.GameFlow, MyApplication.AI, etc.
    // | Some recommend namespaces that reflect the folder structure of the project so it's logically grouped.
    // | Strip unused 'usings' except the 'minimally-required set'
    // ---
    // 《Unity C# 样式指南》
    // 它是受微软启发而制定的适用于 Unity 项目的示例样式指南。
    // 微软的《框架设计指南》（https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/）和
    // Google 维护的样式指南（https://google.github.io/styleguide/csharp-style.html）也可供参考。
    // 需要明确的是，本指南并非规则手册，而是团队讨论并达成共同风格的起点。
    // 由于不存在适用于所有情况的统一样式指南，团队应选择最适合自身和项目的准则。
    // 尽管没有绝对正确的解决方案，但本指南能为团队提供一些准则灵感。
    // 最重要的是保持一致性，若有团队，需就样式指南达成一致并坚持执行。
    // 样式指南的目标是消除编码规范和格式方面的猜测，使团队能专注于解决方案。
    // ---
    // 关键原则
    // 尽可能遵循行业标准和惯例来编写代码，优先考虑代码的可读性而非简洁性，最终目标是使代码更具可读性、可维护性和一致性。
    // 代码风格标准是一份动态文档，在遇到新的问题时，应该商讨确定统一标准。
    // 优先使用自然易读且可发音的命名，不一定使用最专业的命名，例如，MultiLanguage 比 MultiLingual 可能更适合中文开发者。
    // 最重要的是先写出能运行的代码，再考虑让它“整洁”。
    // ---
    // 命名规则
    // 应优先考虑可读性而非简洁性。
    // 变量名应该具有描述性、清晰且明确，因为它们代表一个事物或状态。
    // 在命名时除非变量是布尔类型，否则应使用名词。
    // 对于布尔变量，应在前面加上动词以使其含义更加明显。例如：isDead、isWalking、hasDamageMultiplier。
    // 使用有意义的名称。不要进行缩写（除非是数学运算或被普遍接受的）。您的变量名应能揭示其意图。
    // 给常见的术语添加具体的前缀或者后缀以便准确区分。（例如，使用“PhysicsSolver”，而非“Solver”）
    // ---
    // 大小写与前缀
    // 除非另有说明，否则使用帕斯卡命名法，例如，ExamplePlayerController、MaxHealth
    // 局部变量或者参数使用小驼峰命名法，例如，examplePlayerController、maxHealth
    // MonoBehaviour 的命名要和脚本文件名匹配
    // 私有成员使用小写下划线前缀，例如，_examplePlayerController、_maxHealth
    // 静态成员使用 s_ 前缀，例如，s_examplePlayerController、s_maxHealth
    // 常量成员使用 k_ 前缀，例如，k_examplePlayerController、k_maxHealth
    // 忽略私有访问级别
    // 避免使用冗余的命名，如果你的类名为 Player，你不需要创建名为 PlayerScore 或 PlayerTarget 的成员变量，可简化为 Score 或 Target。
    // ---
    // 格式
    // 左花括号另起新行，一般一行 80 - 120 个字符。尝试拆分多行，而不是一行超过最大字符数。避免在括号内使用空格。
    // 缩进为 4 个空格
    // ---
    // 间距
    // 函数参数之间的逗号后使用单个空格，例如，CollectItem(myObject, 0, 1);
    // 括号和函数参数之间不要添加空格，例如，CollectItem(myObject, 0, 1);
    // 函数名和括号之间不要使用空格，例如，DropPowerUp(myPrefab, 0, 1);
    // 使用垂直间距（额外的空行）进行视觉分隔，例如，for (int i = 0; i < 100; i++) { DoSomething(i); }
    // 大多数情况下每行只声明一个变量。虽然不太紧凑，但提高了可读性。
    // 函数名和括号之间不要使用空格，例如，DropPowerUp(myPrefab, 0, 1);
    // 在流控制条件之前使用单个空格，在比较运算符前后使用单个空格，例如，if (x == y)。
    // ---
    // 注释
    // 减少注释，注释尽量保持 XML 标签和注释有关的特性同步存在，公共方法或函数尽量补充注释，私有方法仅在需要时添加注释，即代码无法自解释的时候。
    // 注释不应仅仅回答“是什么”或“怎么做”，还应填补空白，告诉我们“为什么”。记录“为什么”远比记录“是什么”或“怎么做”重要得多。
    // 避免直接使用归属信息，比如：Created By，尽量依靠版本控制来跟踪更改。
    // 减少使用 #region 块。除非是说明类，演示类工具脚本，否则尽量避免 #region 块，保持单一职责原则。
    // 如果检查器中的序列化字段需要解释，根据情况补充 Tooltip。
    // ---
    // 类的组织
    // 通常保持以下顺序: 字段，属性，事件，Mono 的事件方法，公共方法，私有方法，其他类。或者直接采用 Rider 的 “完全清理”
    // ---
    // 使用语句
    // 将使用语句保持在文件顶部。
    // 删除未使用的行。
    // ---
    // 命名空间：
    // 在不冲突的情况下减少命名空间层数，尽量只做重要区分，比如品牌名，项目名，模块名，Editor 和 Runtime 等，上文的谷歌规范是不超过2层。
    // 不要强制文件/文件夹布局匹配名称空间。
    // 不使用特殊符号，只使用 " . " 字符进行拼接，使用帕斯卡命名法。

    #endregion

    #region Enum

    // ENUMS:
    // | Use enums when an object or action can only have one value at a time.
    // | Use Pascal case for enum names and values.
    // | Use a singular noun for the enum name as it represents a single value from a set of possible values. 
    // | They should have no prefix or suffix.
    // | You can place public enums outside a class to make them global.
    // ---
    // 枚举：
    // 普通枚举是只能是唯一的状态或者值。
    // 使用帕斯卡命名，普通枚举名称使用单数，因为它只代表一个状态或者值。

    #endregion

    public enum Direction
    {
        North,
        South,
        East,
        West
    }

    #region Flag Enum

    // FLAG ENUM:
    // | Use flag enum to represent combinations of options when multiple values can be chosen at the same time, enabling bitwise operations.
    // | Use a plural noun to indicate the possibility of multiple selections (e.g., AttackModes). 
    // | No prefix or suffix.
    // | Use column-alignment for binary values.
    // | Alternatively, consider using the 1 << outnumber style.
    // --- 
    // 标记枚举
    // 当可以同时选择多个值时，使用标志枚举来表示选项的组合，以支持位运算。
    // 使用复数名词来表示可能有多个选择。
    // 补充位运算值

    #endregion

    [Flags]
    public enum AttackModes
    {
        // Decimal // Binary
        // 十进制   // 二进制
        None = 0,                         // 000000
        Melee = 1,                        // 000001
        Ranged = 2,                       // 000010
        Special = 4,                      // 000100
        MeleeAndSpecial = Melee | Special // 000101
    }

    #region Interface

    // INTERFACES:
    // | Interfaces allow you to define a common contract, when unrelated classes need to share common functionality but implement it differently
    // | Prefix interface names with a capital I
    // | Follow this with naming interfaces with adjective phrases that describe the functionality.
    // --- 
    // 接口：
    // 当不相关的类需要共享共同功能但实现方式不同时，接口允许你定义一个共同的契约
    // 接口名称以大写字母 I 为前缀
    // 随后使用描述功能的形容词短语来命名接口。

    #endregion

    public interface IDamageable
    {
        string DamageTypeName { get; }

        float DamageValue { get; }

        bool ApplyDamage(string description, float damage, int numberOfHits);
    }

    public interface IDamageable<in T>
    {
        void Damage(T damageTaken);
    }

    #region Classes or Structs

    // CLASSES or STRUCTS:
    // | Use Pascal case 
    // | Name them with nouns or noun phrases. This distinguishes type names from methods, which are named with verb phrases.
    // | Avoid prefixes.
    // | One MonoBehaviour per file. If you have a MonoBehaviour in a file, the source file name must match. 
    // ---
    // 类或结构体：
    // 使用帕斯卡命名法
    // 使用名词或名词短语命名。这将类型名称与使用动词短语命名的方法区分开来。
    // 继承 MonoBehaviour 的类，类名和源文件名必须匹配。

    #endregion

    public class OdinToolkitsCodeStyleExample : MonoBehaviour
    {
        // 常量使用全大写
        const int MAX_COUNT = 100;

        // 静态使用 Rider 默认
        static int _sharedCount;

        // Use [SerializeField] attribute if you want to display a private field in Inspector.
        // 如果你想在检查器中显示私有字段，请使用 [SerializeField] 属性。
        // Booleans ask a question that can be answered true or false.
        // 布尔型变量表示一个可以用真或假回答的问题。
        [SerializeField]
        bool isPlayerDead;

        // This groups data from the custom PlayerStats class in the Inspector.
        // 这会在检查器中对自定义 PlayerStats 类的数据进行分组。
        [SerializeField]
        PlayerStats stats;

        // Use the Range attribute to set minimum and maximum values. 
        // 使用 Range 属性设置最小值和最大值。
        // This limits the values to a Range and creates a slider in the Inspector.
        // 这会将值限制在一个范围内，并在检查器中创建一个滑块。
        [Range(0f, 1f)]
        [SerializeField]
        float rangedStat;

        // A tooltip can replace a comment on a serialized field and do double duty.
        // 工具提示可以替代序列化字段上的注释，起到双重作用。
        [Tooltip("This is another statistic for the player.")]
        [SerializeField]
        float anotherStat;

        // Some prefer to use an underscore prefix for private fields.
        // 有些人更喜欢为私有字段使用下划线前缀。
        int _elapsedTimeInDays;

        // The private backing field
        // 私有后备字段
        int _maxHealth;

        // Read-only, returns backing field
        // 只读，返回后备字段
        public int MaxHealthReadOnly => _maxHealth;

        // Equivalent to: 
        // 等同于：
        // public int MaxHealth { get; private set; }
        // 公共最大生命值 { 获取; 私有设置; }

        // explicitly implementing getter and setter
        // 显式实现 getter 和 setter
        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        // Write-only (not using backing field)
        // 只写（不使用后备字段）
        public int Health { private get; set; }

        // Auto-implemented property without backing field
        // 没有后备字段的自动实现属性
        public string DescriptionName { get; set; } = "Fireball";

        // Write-only, without an explicit setter
        // 只写，没有显式的 setter
        public void SetMaxHealth(int newMaxValue) => _maxHealth = newMaxValue;

        // Event before
        // 事件前
        public event Action OpeningDoor;

        // Event after
        // 事件后
        public event Action DoorOpened;

        // Event with int parameter
        // 带有整数参数的事件
        public event Action<int> PointsScored;

        // Custom event with custom EventArgs
        // 带有自定义 EventArgs 的自定义事件
        public event Action<CustomEventArgs> ThingHappened;

        // These are event raising methods, e.g. OnDoorOpened, OnPointsScored, etc.
        // Prefix the event raising method (in the subject) with “On”.
        // Alternatively, event handling method e.g. MySubject_DoorOpened().

        public void OnDoorOpened()
        {
            DoorOpened?.Invoke();
        }

        public void OnPointsScored(int points)
        {
            PointsScored?.Invoke(points);
        }

        // Methods start with a verb.
        // 方法以动词开头。
        public void SetInitialPosition(float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }

        // Methods ask a question when they return bool.
        // 返回布尔值的方法应该是一个问题。
        public bool IsNewPosition(Vector3 newPosition) => transform.position == newPosition;

        void FormatExamples(int someExpression)
        {
            #region var 关键字

            // var 关键字：
            // | While avoiding ambiguity and always looking for ways to improve readability, 
            // | you can use var  when the type is clear from the context,
            // | With good naming ambiguity should be less of an issue because variable names already convey the intent.  
            // | Refactoring is simpler with var since it abstracts away the specific type, 
            // | reducing the number of places where code needs to be updated when types change.  
            // | In foreach loops, var ensures that the iteration variable matches the type provided by the enumerator. 
            // | If you explicitly declare a mismatched type, the compiler may allow it, leading to runtime errors.
            // var 关键字：
            // 在避免歧义并始终寻求提高可读性的方法时，当从上下文可以清楚知道类型时，你可以使用 var 关键字。
            // 在 foreach 循环中，var 确保迭代变量与枚举器提供的类型匹配。
            // 如果你显式声明了不匹配的类型，编译器可能会允许，但会导致运行时错误。

            #endregion

            var powerUps = new List<PlayerStats>();

            var dict = new Dictionary<string, List<GameObject>>();

            // SWITCH STATEMENTS:
            // | It’s generally advisable to replace longer if-else chains with a switch statement for better readability.
            // | The formatting can vary. Select one for your style guide and follow it.
            // | This example indents each case and the break underneath.
            // ---
            // switch (condition)
            // 通常建议用开关语句替换较长的 if-else 链，以提高可读性。
            // 对每个 case 进行缩进，并将 break 放在下面。
            switch (someExpression)
            {
                case 0:
                    // ..
                    break;

                case 1:
                    // ..
                    break;

                case 2:
                    // ..
                    break;
            }

            // BRACES: 
            // | Where possible, don’t omit braces, even for single-line statements. 
            // | Or avoid single-line statements entirely for debuggability.
            // | Keep braces in nested multi-line statements.
            // ---
            // 花括号
            // 不要省略花括号，即使是单行语句。循环等完全避免使用单行语句。
            // 在嵌套的多行语句中保留花括号。

            // ... but this is more readable and often more debuggable. 
            // ... 但这更具可读性，并且通常更易于调试。
            for (var i = 0; i < 100; i++)
            {
                DoSomething(i);
            }

            // Separate the statements for readability.
            // 为了可读性，分开语句。
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    DoSomething(j);
                }
            }
        }

        void DoSomething(int x)
        {
            // .. 
        }

        // This is a custom EventArg made from a struct.
        // 这是一个由结构体构成的自定义 EventArg。
        public struct CustomEventArgs
        {
            public int ObjectID { get; }

            public Color Color { get; }

            public CustomEventArgs(int objectId, Color color)
            {
                ObjectID = objectId;
                Color = color;
            }
        }

        public class CustomEventArgsClass : EventArgs { }

        #region Field

        // FIELDS: 
        // | Avoid special characters (backslashes, symbols, Unicode characters); these can interfere with command line tools.
        // | Use nouns for names, but prefix booleans with a verb.
        // | Use meaningful names. Make names searchable and pronounceable. Don’t abbreviate (unless it’s math).
        // | Use Pascal case for public fields. Use camel case for private variables.
        // | Specify (or omit) the default private access modifier but do it consistently. 
        // | We recommend to leave out things that are implicit and thus redundant (such as private) for simplicity 
        // | if you agree that it doesn't negatively affect ambiguity or readability for you.
        // | There are lots of opinions on the use of prefixes. 
        // | Pick what works best for you and the team and be consistent with your style guide.
        // | You can consider adding an underscore (_) in front of private fields to differentiate from local variables
        // | You can alternatively use more explicit prefixes: m_ = member variable, s_ = static, k_ = const 
        // | favoring readability over brevity as the guiding principle.
        // We recommend using explicit prefixes: m_ = member variable which favors specific and 
        // readability above saving a few keystrokes. However, we suggest to leave out "private" as it's implicit for simplicity.  
        // Whatever style you choose do it consistently.
        // ---
        // 字段：
        // 名称使用名词，但布尔型变量以动词为前缀。使用有意义的名称。使名称可搜索且易发音。尽量减少缩写，除非是数学术语。
        // 公共字段使用帕斯卡命名法。私有变量使用下划线前缀+驼峰命名法。
        // 省略私有访问修饰符。

        #endregion

        #region Properties

        // PROPERTIES:
        // | Preferable to a public field.
        // | 比公共字段更可取。
        // | Pascal case, without special characters.
        // | 使用帕斯卡命名法，不使用特殊字符。
        // | Use the expression-bodied properties to shorten, but choose your preferrred format.
        // | 使用表达式体属性来简化，但选择你喜欢的格式。
        // | E.g. use expression-bodied for read-only properties but { get; set; } for everything else.
        // | 例如，只读属性使用表达式体，其他属性使用 { get; set; }。
        // | Use the Auto-Implemented Property for a public property without a backing field.
        // | 对于没有后备字段的公共属性，使用自动实现属性。
        // | While you can also use functions to expose private data properties are generally recommended.
        // | 虽然你也可以使用函数来暴露私有数据，但通常建议使用属性。
        // | For get or set operations involving complex logic or computation, use methods instead of properties.
        // | 对于涉及复杂逻辑或计算的 get 或 set 操作，使用方法而不是属性。
        // --- 
        // 属性：
        // 减少公共字段的使用，如果要对外，尽量使用属性。
        // 对于没有后备字段的公共属性，使用自动实现属性。
        // 虽然你也可以使用函数来暴露私有数据，但通常建议使用属性。
        // 对于涉及复杂逻辑或计算的 get 或 set 操作，使用方法而不是属性。

        #endregion

        #region Events

        // EVENTS:
        // | Name with a verb phrase.
        // | Present participle means "before" and past participle means "after."
        // | Use System.Action delegate for most events (can take 0 to 16 parameters).
        // | Define a custom EventArg only if necessary (either System.EventArgs or a custom struct).
        // | OR alternatively, use the System.EventHandler; choose one and apply consistently.
        // | Choose a naming scheme for events, event handling methods (subscriber/observer), 
        // | and event raising methods (publisher/subject)
        // | e.g. event/action = "OpeningDoor", event raising method = "OnDoorOpened", event handling method = "MySubject_DoorOpened"
        // ---
        // 事件：
        // 使用动词短语命名。
        // 大多数事件使用 System.Action 委托（可以接受 0 到 16 个参数）。
        // 仅在必要时定义自定义 EventArg（可以是 System.EventArgs 或自定义结构体）。
        // 尽量使用 event 标记事件，可以在 IDE 中更方便的显示。使用方法封装事件触发方法。
        // 事件触发方法以 On 开头，事件通常采用动作描述。
        // 例如，事件/动作 = "OpeningDoor"，事件引发方法 = "OnDoorOpened"

        #endregion

        #region Methods

        // METHODS:
        // | While “function” and “method” are often used interchangeably, method is the right term in Unity development
        // | because you can’t write a function without incorporating it into a class in C#.
        // | Start a method name with a verb or verb phrases to show an action. Add context if necessary. e.g. GetDirection, FindTarget, etc.
        // | Methods returning bool should ask questions: Much like Boolean variables themselves.
        // | Prefix methods with a verb if they return a true-false condition. 
        // | This phrases them in the form of a question, e.g. IsGameOver, HasStartedTurn
        // | Use camel case for parameters. Format parameters passed into the method like local variables.
        // | SOME GENERAL TIPS FOR METHODS:
        // | Avoid long methods. If a method is too long, consider breaking it into smaller methods.
        // | Avoid methods with too many parameters. If a method has more than three parameters, 
        // | consider using a class or struct to group them.
        // | Avoid excessive overloading: You can generate an endless permutation of method overloads.
        // | Avoid side effects: A method only needs to do what its name advertises.
        // | A good name for a method reflects what it does.
        // | Avoid setting up methods to work in multiple different modes based on a flag. 
        // | Make two methods with distinct names instead, e.g. GetAngleInDegrees and GetAngleInRadians. 
        // ---
        // 方法：
        // 虽然“函数”几乎等同于“方法”，但在 Unity 开发中，“方法”是正确的术语，因为在 C# 中，必须在类中才能编写函数（方法）
        // 方法名以动词或动词短语开头，以表示一个动作。必要时添加上下文。例如，GetDirection、FindTarget 等。
        // 返回布尔值的方法应该是一个问题：就像布尔型变量本身一样。如果方法返回布尔条件，以动词为前缀。
        // 这会将它们表述为问题的形式，例如，IsGameOver、HasStartedTurn
        // 避免使用长方法。如果一个方法太长，考虑将其拆分成更小的方法。
        // 避免使用参数过多的方法。如果一个方法有超过三个参数，考虑使用类或结构体来对它们进行分组。
        // 避免过度重载：你可以生成无穷无尽的方法重载组合。
        // 避免副作用：一个方法只需要做它名称所表明的事情。一个好的方法名能反映它的功能。
        // 避免根据标志设置方法以多种不同模式工作。而是创建两个名称不同的方法，例如，GetAngleInDegrees 和 GetAngleInRadians。

        #endregion
    }

    // OTHER CLASSES:
    // | Define as many other helper/non-MonoBehaviour classes in your file as needed.
    // | This is a serializable class that groups fields in the Inspector.
    // ---
    // 其他类：
    // 这是一个可序列化的类，它在检查器中对字段进行分组。
    // 根据需要在你的文件中定义尽可能多的其他辅助/非 MonoBehaviour 类。
    [Serializable]
    public struct PlayerStats
    {
        public int movementSpeed;
        public int hitPoints;
        public bool hasHealthPotion;
    }
}
