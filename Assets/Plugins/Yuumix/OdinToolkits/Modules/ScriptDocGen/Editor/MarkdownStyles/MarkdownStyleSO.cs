using System.Text;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Editor;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor
{
    public abstract class MarkdownStyleSO : ScriptableObject
    {
        public virtual string GetMarkdownText(TypeData typeData, DocCategory docCategory, StringBuilder identifier)
        {
            StringBuilder headerIntroduction = CreateHeaderIntroduction(typeData);
            StringBuilder constructorsContent = CreateConstructorsContent(typeData, docCategory);
            StringBuilder methodsContent = CreateCurrentMethodsContent(typeData, docCategory);
            StringBuilder eventsContent = CreateCurrentEventsContent(typeData, docCategory);
            StringBuilder propertiesContent = CreateCurrentPropertiesContent(typeData, docCategory);
            StringBuilder fieldsContent = CreateCurrentFieldsContent(typeData, docCategory);
            StringBuilder inheritedContent = CreateInheritanceContent(typeData, docCategory);
            StringBuilder finalStringBuilder = headerIntroduction
                .Append(constructorsContent)
                .Append(methodsContent)
                .Append(eventsContent)
                .Append(propertiesContent)
                .Append(fieldsContent)
                .Append(inheritedContent)
                .Append(identifier);
            return finalStringBuilder.ToString();
        }

        protected abstract StringBuilder CreateHeaderIntroduction(TypeData typeData);
        protected abstract StringBuilder CreateConstructorsContent(TypeData typeData, DocCategory docCategory);
        protected abstract StringBuilder CreateCurrentMethodsContent(TypeData typeData, DocCategory docCategory);
        protected abstract StringBuilder CreateCurrentEventsContent(TypeData typeData, DocCategory docCategory);
        protected abstract StringBuilder CreateCurrentPropertiesContent(TypeData typeData, DocCategory docCategory);
        protected abstract StringBuilder CreateCurrentFieldsContent(TypeData typeData, DocCategory docCategory);
        protected abstract StringBuilder CreateInheritanceContent(TypeData typeData, DocCategory docCategory);
    }
}
