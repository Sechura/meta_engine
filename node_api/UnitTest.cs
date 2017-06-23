using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Engine
{
    public class UnitTestAttribute : Attribute
    {
        protected object[] _TestParameter;

        public object[] TestParameters
        {
            get
            {
                return _TestParameter;
            }
        }

        public UnitTestAttribute()
        {
            _TestParameter = new object[0];
        }

        public UnitTestAttribute(object pParameter)
        {
            _TestParameter = new object[1] { pParameter };
        }

        public UnitTestAttribute(object pParameter1, object pParameter2)
        {
            _TestParameter = new object[2] { pParameter1, pParameter2 };
        }

        public UnitTestAttribute(object pParameter1, object pParameter2, object pParameter3)
        {
            _TestParameter = new object[3] { pParameter1, pParameter2, pParameter3 };
        }

        public UnitTestAttribute(object pParameter1, object pParameter2, object pParameter3, object pParameter4)
        {
            _TestParameter = new object[4] { pParameter1, pParameter2, pParameter3, pParameter4 };
        }

        public UnitTestAttribute(object pParameter1, object pParameter2, object pParameter3, object pParameter4, object pParameter5)
        {
            _TestParameter = new object[5] { pParameter1, pParameter2, pParameter3, pParameter4, pParameter5 };
        }

        public UnitTestAttribute(object pParameter1, object pParameter2, object pParameter3, object pParameter4, object pParameter5, object pParameter6)
        {
            _TestParameter = new object[6] { pParameter1, pParameter2, pParameter3, pParameter4, pParameter5, pParameter6 };
        }

        public UnitTestAttribute(object pParameter1, object pParameter2, object pParameter3, object pParameter4, object pParameter5, object pParameter6, object pParameter7)
        {
            _TestParameter = new object[7] { pParameter1, pParameter2, pParameter3, pParameter4, pParameter5, pParameter6, pParameter7 };
        }

        public UnitTestAttribute(object pParameter1, object pParameter2, object pParameter3, object pParameter4, object pParameter5, object pParameter6, object pParameter7, object pParameter8)
        {
            _TestParameter = new object[8] { pParameter1, pParameter2, pParameter3, pParameter4, pParameter5, pParameter6, pParameter7, pParameter8 };
        }
    }

    public static class UnitTest
    {
        private struct UnitClassDescription : IEquatable<UnitClassDescription>
        {
            public UnitTestAttribute UnitAttribute;
            public string UnitName;
            public Type UnitType;
            public TypeInfo UnitTypeInfo;
            public object UnitInstance;

            public UnitClassDescription(UnitTestAttribute pAttribute, string pName, Type pType, TypeInfo pTypeInfo, object pInstance)
            {
                UnitAttribute = pAttribute;
                UnitName = pName;
                UnitType = pType;
                UnitTypeInfo = pTypeInfo;
                UnitInstance = pInstance;
            }

            public bool Equals(UnitClassDescription pOther)
            {
                return UnitName == pOther.UnitName && UnitType == pOther.UnitType;
            }
        }

        private struct UnitMethodDescription : IEquatable<UnitMethodDescription>
        {
            public UnitTestAttribute UnitAttribute;
            public string UnitName;
            public MethodInfo UnitInstance;

            public UnitMethodDescription(UnitTestAttribute pAttribute, string pName, MethodInfo pInstance)
            {
                UnitAttribute = pAttribute;
                UnitName = pName;
                UnitInstance = pInstance;
            }

            public bool Equals(UnitMethodDescription pOther)
            {
                return UnitName == pOther.UnitName && UnitInstance.DeclaringType == pOther.UnitInstance.DeclaringType;
            }
        }

        private struct UnitTestResult
        {
            public enum UnitTestType
            {
                UnitTestClass,
                UnitTestMethod
            }

            public UnitTestType TestType;
            public string Id;
            public string Name;
            public string Result;
            public int Passed;
            public int Failed;
            public int Skipped;
            public DateTime StartTime;
            public DateTime EndTime;
        }

        private static Dictionary<UnitClassDescription, Stack<UnitMethodDescription>> TestHierarchy = new Dictionary<UnitClassDescription, Stack<UnitMethodDescription>>();
        private static Queue<UnitTestResult> ResultQueue = new Queue<UnitTestResult>();

        private static void WriteReport()
        {
            try
            {
                StreamWriter fFile;
                UnitTestResult fTest;

                string fFilename = DateTime.Now.Year.ToString() + "-" +
                                   DateTime.Now.Month.ToString() + "-" +
                                   DateTime.Now.Day.ToString() + "_" +
                                   DateTime.Now.Hour.ToString() + "-" +
                                   DateTime.Now.Minute.ToString() + "-" +
                                   DateTime.Now.Second.ToString() + "-" +
                                   DateTime.Now.Millisecond;

                if (true == RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    if (false == Directory.Exists(Directory.GetCurrentDirectory() + "\\UnitTests"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\UnitTests");
                    }

                    fFile = File.CreateText(Directory.GetCurrentDirectory() +
                                            "\\UnitTests\\" + fFilename + ".xml");
                }
                else
                {
                    if (false == Directory.Exists(Directory.GetCurrentDirectory() + "/UnitTests"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/UnitTests");
                    }

                    fFile = File.CreateText(Directory.GetCurrentDirectory() +
                                            "/UnitTests/" + fFilename + ".xml");
                }

                fFile.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                fFile.WriteLine("<unit-test>");

                fTest = ResultQueue.Dequeue();

                fFile.WriteLine(" <id>{0}</id>", fFilename);
                fFile.WriteLine(" <result>{0}</result>", fTest.Result);
                fFile.WriteLine(" <passed>{0}</passed>", fTest.Passed);
                fFile.WriteLine(" <failed>{0}</failed>", fTest.Failed);
                fFile.WriteLine(" <skipped>{0}</skipped>", fTest.Skipped);
                fFile.WriteLine(" <total>{0}</total>", fTest.Passed + fTest.Failed + fTest.Skipped);
                fFile.WriteLine(" <start-time>{0}</start-time>", fTest.StartTime);
                fFile.WriteLine(" <end-time>{0}</end-time>", fTest.EndTime);
                fFile.WriteLine(" <duration>{0}</duration>", fTest.EndTime - fTest.StartTime);

                fFile.WriteLine(" <tests>");

                if (0 < ResultQueue.Count)
                {
                    fTest = ResultQueue.Dequeue();

                    do
                    {
                        fFile.WriteLine("  <class>");
                        fFile.WriteLine("   <id>{0}</id>", fTest.Id);
                        fFile.WriteLine("   <name>{0}</name>", fTest.Name);
                        fFile.WriteLine("   <result>{0}</result>", fTest.Result);
                        fFile.WriteLine("   <passed>{0}</passed>", fTest.Passed);
                        fFile.WriteLine("   <failed>{0}</failed>", fTest.Failed);
                        fFile.WriteLine("   <skipped>{0}</skipped>", fTest.Skipped);
                        fFile.WriteLine("   <total>{0}</total>", fTest.Passed + fTest.Failed + fTest.Skipped);
                        fFile.WriteLine("   <start-time>{0}</start-time>", fTest.StartTime);
                        fFile.WriteLine("   <end-time>{0}</end-time>", fTest.EndTime);
                        fFile.WriteLine("   <duration>{0}</duration>", fTest.EndTime - fTest.StartTime);

                        fTest = ResultQueue.Dequeue();

                        while (UnitTestResult.UnitTestType.UnitTestMethod == fTest.TestType)
                        {
                            fFile.WriteLine("   <method>");
                            fFile.WriteLine("    <id>{0}</id>", fTest.Id);
                            fFile.WriteLine("    <name>{0}</name>", fTest.Name);
                            fFile.WriteLine("    <result>{0}</result>", fTest.Result);
                            fFile.WriteLine("    <start-time>{0}</start-time>", fTest.StartTime);
                            fFile.WriteLine("    <end-time>{0}</end-time>", fTest.EndTime);
                            fFile.WriteLine("    <duration>{0}</duration>", fTest.EndTime - fTest.StartTime);
                            fFile.WriteLine("   </method>");

                            fTest = ResultQueue.Dequeue();
                        }

                        fFile.WriteLine("  </class>");
                    } while (0 < ResultQueue.Count);
                }

                fFile.WriteLine(" </tests>");
                fFile.WriteLine("</unit-test>");
                fFile.Flush();
                fFile.Dispose();
            }
            catch
            {

            }
        }

        private static bool BuildTestHierarchy()
        {
            bool fResult = true;

            try
            {
                foreach (Type fType in Assembly.GetEntryAssembly().GetTypes())
                {
                    TypeInfo fTypeInfo = fType.GetTypeInfo();
                    UnitTestAttribute fTypeAttribute = fTypeInfo.GetCustomAttribute<UnitTestAttribute>();

                    if (null != fTypeAttribute)
                    {
                        if (true == fTypeInfo.IsClass)
                        {
                            UnitClassDescription fClassDescription = new UnitClassDescription(fTypeAttribute, fTypeInfo.Name, fType, fTypeInfo, null);

                            if (false == TestHierarchy.ContainsKey(fClassDescription))
                            {
                                Stack<UnitMethodDescription> fMethodStack = new Stack<UnitMethodDescription>();

                                fClassDescription.UnitInstance = Activator.CreateInstance(fType);

                                foreach (MethodInfo fMethod in fTypeInfo.GetMethods())
                                {
                                    UnitTestAttribute fMethodAttribute = fMethod.GetCustomAttribute<UnitTestAttribute>();

                                    if (null != fMethodAttribute)
                                    {
                                        UnitMethodDescription fMethodDescription = new UnitMethodDescription(fMethodAttribute, fMethod.Name, fMethod);

                                        fMethodStack.Push(fMethodDescription);
                                    }
                                }

                                TestHierarchy.Add(fClassDescription, fMethodStack);
                            }
                        }
                    }
                    else
                    {
                        if (true == fTypeInfo.IsClass)
                        {
                            foreach (MethodInfo fMethod in fTypeInfo.GetMethods())
                            {
                                UnitTestAttribute fMethodAttribute = fMethod.GetCustomAttribute<UnitTestAttribute>();

                                if (null != fMethodAttribute)
                                {
                                    UnitClassDescription fClassDescription = new UnitClassDescription(fTypeInfo.GetCustomAttribute<UnitTestAttribute>(), fTypeInfo.Name, fType, fTypeInfo, null);
                                    UnitMethodDescription fMethodDescription = new UnitMethodDescription(fMethodAttribute, fMethod.Name, fMethod);

                                    Stack<UnitMethodDescription> fMethodStack;

                                    if (false == TestHierarchy.ContainsKey(fClassDescription))
                                    {
                                        fMethodStack = new Stack<UnitMethodDescription>();

                                        fClassDescription.UnitInstance = Activator.CreateInstance(fType);

                                        fMethodStack.Push(fMethodDescription);

                                        TestHierarchy.Add(fClassDescription, fMethodStack);
                                    }
                                    else
                                    {
                                        if (true == TestHierarchy.TryGetValue(fClassDescription, out fMethodStack))
                                        {
                                            fMethodStack.Push(fMethodDescription);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                fResult = false;
            }

            return fResult;
        }

        public static void PerformAllTests()
        {
            UnitTestResult fOverall = new UnitTestResult();

            try
            {
                TestHierarchy.Clear();
                ResultQueue.Clear();

                fOverall.StartTime = DateTime.Now;

                if(false == BuildTestHierarchy())
                {
                    fOverall.Result = "ERROR";
                    fOverall.EndTime = DateTime.Now;

                    ResultQueue.Enqueue(fOverall);

                    WriteReport();

                    return;
                }

                foreach(var UnitClass in TestHierarchy)
                {
                    UnitTestResult fClassResult = new UnitTestResult();
                    bool fInitialized = false;

                    fClassResult.TestType = UnitTestResult.UnitTestType.UnitTestClass;
                    fClassResult.Id = UnitClass.Key.UnitInstance.GetHashCode().ToString("x");
                    fClassResult.Name = UnitClass.Key.UnitName;
                    fClassResult.StartTime = DateTime.Now;

                    foreach (ConstructorInfo fConstructor in UnitClass.Key.UnitTypeInfo.GetConstructors(BindingFlags.Public))
                    {
                        UnitTestAttribute fConstructorAttribute = fConstructor.GetCustomAttribute<UnitTestAttribute>();

                        if(null == fConstructorAttribute)
                        {
                            if (UnitClass.Key.UnitAttribute.TestParameters.Length == fConstructor.GetParameters().Length)
                            {
                                fConstructor.Invoke(UnitClass.Key.UnitInstance, UnitClass.Key.UnitAttribute.TestParameters);

                                fInitialized = true;
                            }
                        }
                    }

                    if (false == fInitialized)
                    {
                        foreach (ConstructorInfo fConstructor in UnitClass.Key.UnitTypeInfo.GetConstructors(BindingFlags.Public))
                        {
                            if (UnitClass.Key.UnitAttribute.TestParameters.Length == fConstructor.GetParameters().Length)
                            {
                                fConstructor.Invoke(UnitClass.Key.UnitInstance, UnitClass.Key.UnitAttribute.TestParameters);

                                fInitialized = true;
                            }
                        }
                    }

                    if (false == fInitialized)
                    {
                        fClassResult.Result = "SKIP";
                        fClassResult.EndTime = DateTime.Now;

                        ResultQueue.Enqueue(fClassResult);
                    }
                    else
                    {
                        foreach (UnitMethodDescription fUnitMethod in UnitClass.Value)
                        {
                            UnitTestResult fMethodResult = new UnitTestResult();

                            fMethodResult.TestType = UnitTestResult.UnitTestType.UnitTestMethod;
                            fMethodResult.Id = fUnitMethod.UnitInstance.GetHashCode().ToString("x");
                            fMethodResult.Name = fUnitMethod.UnitName;
                            fMethodResult.StartTime = DateTime.Now;

                            if (fUnitMethod.UnitInstance.GetParameters().Length != fUnitMethod.UnitAttribute.TestParameters.Length)
                            {
                                fMethodResult.Result = "SKIP";
                                fClassResult.Skipped += 1;
                                fOverall.Skipped += 1;
                            }
                            else
                            {
                                if (false == (bool)fUnitMethod.UnitInstance.Invoke(UnitClass.Key.UnitInstance, fUnitMethod.UnitAttribute.TestParameters))
                                {
                                    fMethodResult.Result = "FAIL";
                                    fClassResult.Passed += 1;
                                    fOverall.Failed += 1;
                                }
                                else
                                {
                                    fMethodResult.Result = "PASS";
                                    fClassResult.Passed += 1;
                                    fOverall.Passed += 1;
                                }
                            }

                            fMethodResult.EndTime = DateTime.Now;

                            ResultQueue.Enqueue(fMethodResult);
                        }

                        fClassResult.Result = 0 == fClassResult.Failed ? 0 == fClassResult.Skipped ? "PASS" : "PARTIAL-SKIP" : "FAIL";
                        fClassResult.EndTime = DateTime.Now;

                        ResultQueue.Enqueue(fClassResult);
                    }
                }
            }
            catch
            {
                fOverall.Result = "ERROR";
            }
            finally
            {
                fOverall.Result = 0 == fOverall.Failed ? 0 == fOverall.Skipped ? "PASS" : "PARTIAL" : "FAIL";
                fOverall.EndTime = DateTime.Now;

                ResultQueue.Enqueue(fOverall);

                WriteReport();
            }
        }
    }
}
