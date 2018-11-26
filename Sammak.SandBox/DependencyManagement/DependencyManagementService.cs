using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Sammak.SandBox.DependencyManagement
{
    public class DependencyManagementService
    {
        //private static Logger _logger;

        public static IWindsorContainer Register(string dependenciesXmlPath)
        {
            //DependencyManagementService._logger = new Logger();
            try
            {
                WindsorContainer windsorContainer = new WindsorContainer();
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(dependenciesXmlPath);
                if (xmlDocument.DocumentElement == null)
                    throw new Exception("Must have a root element in the Dependencies XML document.");

                foreach (XmlNode childNode in xmlDocument.DocumentElement.ChildNodes)
                {
                    if (childNode.Attributes != null)
                    {
                        string innerText1 = childNode.Attributes["Interface"]?.InnerText;
                        if (string.IsNullOrEmpty(innerText1))
                            throw new Exception("You must define the 'Interface' attribute of each dependency node");

                        Type type1 = Type.GetType(innerText1);
                        if (type1 == null)
                            throw new Exception("Could not find Interface " + innerText1);

                        string innerText2 = childNode.Attributes["Implementation"]?.InnerText;
                        if (string.IsNullOrEmpty(innerText2))
                            throw new Exception("You must define the 'Implementation' attribute of each dependency node");

                        Type type2 = Type.GetType(innerText2);
                        if (type2 == null)
                            throw new Exception("Could not find class " + innerText2);

                        /*
                        // additional registration can be done like this:
                        windsorContainer.Register(Component.For<IInternalQueueHandler>().ImplementedBy<InternalQueueHandler>());

                        ComponentRegistration<object> componentRegistration = Component.For(type1).ImplementedBy(type2);
                        var xx = new IRegistration[1]
                        {
                              Component.For(type1).ImplementedBy(type2)
                        };
                        */
                        windsorContainer.Register(new IRegistration[1]
                        {
                              Component.For(type1).ImplementedBy(type2)
                        });
                    }
                }
                return (IWindsorContainer)windsorContainer;
            }
            catch (Exception ex)
            {
                //DependencyManagementService._logger.Error((object)("Failed to register dependencies. XmlPath: " + dependenciesXmlPath), ex);
                throw ex;
            }
        }

        public static Dictionary<Type, Type> InterfaceImplementers(string dependenciesXmlPath)
        {
            var imp = new Dictionary<Type, Type>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(dependenciesXmlPath);
            if (xmlDocument.DocumentElement == null)
                throw new Exception("Must have a root element in the Dependencies XML document.");

            foreach (XmlNode childNode in xmlDocument.DocumentElement.ChildNodes)
            {
                if (childNode.Attributes != null)
                {
                    string innerText1 = childNode.Attributes["Interface"]?.InnerText;
                    if (string.IsNullOrEmpty(innerText1))
                        throw new Exception("You must define the 'Interface' attribute of each dependency node");

                    Type type1 = Type.GetType(innerText1);
                    if (type1 == null)
                        throw new Exception("Could not find Interface " + innerText1);

                    string innerText2 = childNode.Attributes["Implementation"]?.InnerText;
                    if (string.IsNullOrEmpty(innerText2))
                        throw new Exception("You must define the 'Implementation' attribute of each dependency node");

                    Type type2 = Type.GetType(innerText2);
                    imp[type1] = type2 ?? throw new Exception("Could not find class " + innerText2);
                }
            }
            return imp;
        }

        public static Dictionary<string, string> InterfaceImplementersNames(string dependenciesXmlPath)
        {
            var imp = new Dictionary<string, string>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(dependenciesXmlPath);
            if (xmlDocument.DocumentElement == null)
                throw new Exception("Must have a root element in the Dependencies XML document.");

            foreach (XmlNode childNode in xmlDocument.DocumentElement.ChildNodes)
            {
                if (childNode.Attributes != null)
                {
                    string interfaceName = childNode.Attributes["Interface"]?.InnerText;
                    if (string.IsNullOrEmpty(interfaceName))
                        throw new Exception("You must define the 'Interface' attribute of each dependency node");

                    string implementerName = childNode.Attributes["Implementation"]?.InnerText;
                    if (string.IsNullOrEmpty(implementerName))
                        throw new Exception("You must define the 'Implementation' attribute of each dependency node");

                    imp[interfaceName] =  implementerName;
                }
            }
            return imp;
        }
    }
}
