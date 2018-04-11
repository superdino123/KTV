using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Helpers
{
    public class LinqToXml
    {
        #region 构造
        /// <summary>
        /// 用LinqToXml的方式操作XML
        /// 特别要说明的是 LinqToXml中每个xml节点都是一个元素（Element）
        /// 所有操作最后必须调用 SaveXmlFile方法才能更新到文件
        /// 否则只是修改内存中的数据
        /// </summary>
        public LinqToXml()
        {
        }

        #endregion

        #region 创建新xml

        /// <summary>
        /// 创建一个新的xml文档
        /// </summary>
        /// <param name="rootElementName"></param>
        /// <returns></returns>
        public static XElement Create(string rootElementName)
        {
            return new XElement(rootElementName);
        }

        /// <summary>
        /// 创建一个新的xml文档
        /// </summary>
        /// <param name="rootElementName"></param>
        /// <param name="rootElementValue"></param>
        /// <returns></returns>
        public static XElement Create(string rootElementName, object rootElementValue)
        {
            return new XElement(rootElementName, rootElementValue);
        }

        /// <summary>
        /// 根据已有的xelement对象创建xml文档
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static XElement Create(XElement element)
        {
            return new XElement(element);
        }

        /// <summary>
        /// 用指定的名称和内容初始化 XElement 类的新实例
        /// </summary>
        /// <param name="rootElementName"></param>
        /// <param name="objArray"></param>
        /// <returns></returns>
        public static XElement Create(string rootElementName, params object[] objArray)
        {
            return new XElement(rootElementName, objArray);
        }

        /// <summary>
        /// 创建指定根节点名称的XDocument对象
        /// 很少需要创建 XDocument， 而是通常使用 XElement 根节点创建 XML 树。 除非具有创建文档的具体要求（例如，必须在顶级创建处理指令和注释，或者必须支持文档类型），否则使用 XElement 作为根节点通常会更方便。
        /// </summary>
        /// <param name="rootXElement"></param>
        /// <returns></returns>
        public static XDocument CreateXDocument(XElement rootXElement)
        {
            var doc = new XDocument();
            doc.Add(rootXElement);
            return doc;
        }

        /// <summary>
        /// 创建带有Comment信息和根节点的XDcument对象。
        /// 很少需要创建 XDocument，而是通常使用XElement根节点创建XML树。除非具有创建文档的具体要求（例如，必须在顶级创建处理指令和注释，或者必须支持文档类型），否则使用 XElement 作为根节点通常会更方便。
        /// </summary>
        /// <param name="rootElementComment"></param>
        /// <param name="rootXElement"></param>
        /// <returns></returns>
        public static XDocument CreateXDocument(string rootElementComment, XElement rootXElement)
        {
            var doc = new XDocument();
            doc.Add(new XComment(rootElementComment));
            doc.Add(rootXElement);
            return doc;
        }

        /// <summary>
        /// 根据content创建XDcument对象。
        /// 很少需要创建 XDocument，而是通常使用XElement根节点创建XML树。除非具有创建文档的具体要求（例如，必须在顶级创建处理指令和注释，或者必须支持文档类型），否则使用 XElement 作为根节点通常会更方便。
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static XDocument CreateXDocument(params object[] content)
        {
            return new XDocument(content);
        }

        /// <summary>
        /// 根据现有的XDocument对象创建XDcument对象。
        /// 很少需要创建 XDocument，而是通常使用XElement根节点创建XML树。除非具有创建文档的具体要求（例如，必须在顶级创建处理指令和注释，或者必须支持文档类型），否则使用 XElement 作为根节点通常会更方便。
        /// </summary>
        /// <param name="otherDoc"></param>
        /// <returns></returns>
        public static XDocument CreateXDocument(XDocument otherDoc)
        {
            return new XDocument(otherDoc);
        }

        /// <summary>
        /// 用指定的 XDeclaration 和内容初始化 XDocument 类的新XDocument实例。
        /// 很少需要创建 XDocument，而是通常使用XElement根节点创建XML树。除非具有创建文档的具体要求（例如，必须在顶级创建处理指令和注释，或者必须支持文档类型），否则使用 XElement 作为根节点通常会更方便。
        /// </summary>
        /// <param name="declaration"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static XDocument CreateXDocument(XDeclaration declaration, params object[] content)
        {
            return new XDocument(declaration, content);
        }

        #endregion

        #region  保存XML文件

        /// <summary>
        /// 保存XML文件
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <param name="element"></param>
        public static void SaveXml(string xmlFilePath, XElement element)
        {
            element.Save(xmlFilePath);
        }

        /// <summary>
        /// 保存XML文件,指定保存选项设置
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <param name="element"></param>
        /// <param name="options"></param>
        public static void SaveXml(string xmlFilePath, XElement element, SaveOptions options)
        {
            element.Save(xmlFilePath, options);
        }

        /// <summary>
        /// 将Xelement保存到stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="element"></param>
        public static void SaveXml(ref Stream stream, XElement element)
        {
            element.Save(stream);
        }

        /// <summary>
        /// 将Xelement保存到stream，并指定保存选项设置
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="element"></param>
        /// <param name="options"></param>
        public static void SaveXml(ref Stream stream, XElement element, SaveOptions options)
        {
            element.Save(stream, options);
        }

        /// <summary>
        /// 将XDocument保存到文件
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <param name="xDoc"></param>
        public static void SaveXml(string xmlFilePath, XDocument xDoc)
        {
            xDoc.Save(xmlFilePath);
        }

        /// <summary>
        /// 将XDocument保存到文件,指定保存选项
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <param name="xDoc"></param>
        /// <param name="options"></param>
        public static void SaveXml(string xmlFilePath, XDocument xDoc, SaveOptions options)
        {
            xDoc.Save(xmlFilePath, options);
        }


        #endregion

        #region 加载xml文件到XElement

        /// <summary>
        /// 加载xml文件到XElement
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static XElement Load(string path)
        {
            return XElement.Load(path);
        }

        /// <summary>
        /// 加载xml文件到XElement
        /// </summary>
        /// <param name="path"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static XElement Load(string path, LoadOptions options)
        {
            return XElement.Load(path, options);
        }

        /// <summary>
        /// 将stream加载Xelement
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static XElement Load(Stream stream)
        {
            return XElement.Load(stream);
        }

        /// <summary>
        /// 将stream加载Xelement
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static XElement Load(Stream stream, LoadOptions options)
        {
            return XElement.Load(stream, options);
        }

        /// <summary>
        /// 加载xml文件到Xdocument
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static XDocument LoadXDocument(string path)
        {
            return XDocument.Load(path);
        }

        /// <summary>
        /// 加载xml文件到Xdocument
        /// </summary>
        /// <param name="path"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static XDocument LoadXDocument(string path, LoadOptions options)
        {
            return XDocument.Load(path, options);
        }

        /// <summary>
        /// 将stream加载到XDocument
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static XDocument LoadXDocument(Stream stream)
        {
            return XDocument.Load(stream);
        }

        /// <summary>
        /// 将stream加载到XDocument
        /// </summary>
        /// <param name="straem"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static XDocument LoadXDocument(Stream straem, LoadOptions options)
        {
            return XDocument.Load(straem, options);
        }

        #endregion

        #region 解析字符串

        /// <summary>
        /// 将字符串解析成xElement
        /// </summary>
        /// <param name="xmlText"></param>
        /// <returns></returns>
        public static XElement Parse(string xmlText)
        {
            return XElement.Parse(xmlText);
        }

        /// <summary>
        /// 将字符串解析成xElement
        /// </summary>
        /// <param name="xmlText"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static XElement Parse(string xmlText, LoadOptions options)
        {
            return XElement.Parse(xmlText, options);
        }

        /// <summary>
        /// 将字符串解析成XDocument
        /// </summary>
        /// <param name="xmlText"></param>
        /// <returns></returns>
        public static XDocument ParseXDocument(string xmlText)
        {
            return XDocument.Parse(xmlText);
        }

        /// <summary>
        /// 江字符串解析成XDocument
        /// </summary>
        /// <param name="xmlText"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static XDocument ParseXDocument(string xmlText, LoadOptions options)
        {
            return XDocument.Parse(xmlText, options);
        }

        #endregion

        #region 添加元素

        /// <summary>
        /// 批量添加子级元素
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="childElements">new XElement("节点名称", new XAttribute("节点属性", 节点属性),  new XElement("子节点", new XAttribute("节点属性", 节点属性))，无限添加子节点   );</param>
        public static void Add(ref XElement parentElement, IEnumerable<XElement> childElements)
        {
            parentElement?.Add(childElements);
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="childElement">new XElement("节点名称", new XAttribute("节点属性", 节点属性),  new XElement("子节点", new XAttribute("节点属性", 节点属性))，无限添加子节点   );</param>
        public static void Add(ref XElement parentElement, XElement childElement)
        {
            parentElement?.Add(childElement);
        }
        #endregion

        #region 删除元素

        /// <summary>
        /// 根据元素名称删除子元素
        /// </summary>
        /// <param name="element"></param>
        /// <param name="childElementName"></param>
        /// <param name="removeAll"></param>
        public static void RemoveChild(ref XElement element, string childElementName, bool removeAll = false)
        {
            if (!removeAll)
                element?.Element(childElementName)?.Remove();
            else
            {
                var coll = element?.Elements(childElementName);
                if (null == coll)
                    return;
                foreach (var item in coll)
                {
                    item.Remove();
                }
            }
        }

        /// <summary>
        /// 根据元素名称删除子孙代元素
        /// </summary>
        /// <param name="element"></param>
        /// <param name="offspringElementName"></param>
        /// <param name="removeAll"></param>
        public static void RemoveOffspring(ref XElement element, string offspringElementName, bool removeAll = false)
        {
            if (!removeAll)
            {
                element?.Descendants(offspringElementName)?.FirstOrDefault()?.Remove();
            }
            else
            {
                var coll = element?.Descendants(offspringElementName);
                if (null == coll) return;
                foreach (var item in coll)
                {
                    item.Remove();
                }
            }
        }

        #endregion

        #region 修改

        /// <summary>
        /// 修改特定名称的子元素的值
        /// </summary>
        /// <param name="element"></param>
        /// <param name="childElementName"></param>
        /// <param name="childElementValue"></param>
        /// <param name="modifyAll">指定是否修改所有满足指定名的子元素</param>
        public static void ModifyChild(ref XElement element, string childElementName, object childElementValue, bool modifyAll = false)
        {
            if (!modifyAll)
            {
                element?.Element(childElementName)?.SetValue(childElementValue);
            }
            else
            {
                var coll = element?.Elements(childElementName);
                if (null == coll) return;
                foreach (var item in coll)
                {
                    item.SetValue(childElementValue);
                }
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 根据元素名称查询子元素,只返回第一个
        /// </summary>
        /// <param name="element"></param>
        /// <param name="queryElementId"></param>
        public static XElement QueryElemnet(XElement element, string queryElementId, bool queryAll = false)
        {
            return element?.Element(queryElementId);
        }

        /// <summary>
        /// 根据元素名称批量查询元素
        /// </summary>
        /// <param name="element"></param>
        /// <param name="queryElementId"></param>
        public static IEnumerable<XElement> QueryElements(XElement element, string queryElementId)
        {
            return element?.Elements(queryElementId);
        }

        /// <summary>
        /// 根据元素名和属性名称批量查询元素
        /// </summary>
        /// <param name="element">源</param>
        /// <param name="queryElementId"></param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        /// <returns></returns>
        public static IEnumerable<XElement> QueryElements(XElement element, string queryElementId, string attributeName, string attributeValue)
        {
            return element?.Elements(queryElementId)?.Where(i => i.Attribute(attributeName)?.Value == attributeValue);
        }

        /// <summary>
        /// 按照文档顺序返回指定名称的子孙代元素的集合
        /// </summary>
        /// <param name="element"></param>
        /// <param name="elemnetName"></param>
        /// <returns></returns>
        public static IEnumerable<XElement> Descendants(XElement element, string elemnetName)
        {
            return element?.Descendants(elemnetName);
        }

        /// <summary>
        /// 按照文档顺序返回指定名称的子孙代元素的集合
        /// </summary>
        /// <param name="element"></param>
        /// <param name="elemnetName"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <returns></returns>
        public static IEnumerable<XElement> Descendants(XElement element, string elemnetName, string attributeName, string attributeValue)
        {
            return element?.Descendants(elemnetName)?.Where(i => i.Attribute(attributeName)?.Value == attributeValue);
        }

        #endregion

    }
}
