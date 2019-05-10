using ErrorLog;
using System;
using Timor.HomeWork.Factory;
using Timor.HomeWork.IService;
using Timor.HomeWork.Model;
using Timor.HomeWork.Util;

namespace Timor.HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                {
                    Console.WriteLine("***************给一个实体打印出对应属性和值***************");
                    Company c = new Company()
                    {
                        CreateTime = DateTime.Now,
                        CreatorId = 1,
                        Id = 1,
                        LastModifierId = 1,
                        LastModifyTime = DateTime.Now,
                        Name = "asd"
                    };
                    Company c1 = new Company()
                    {
                        CreateTime = DateTime.Now,
                        CreatorId = 1,
                        Id = 1,
                        LastModifierId = 1,
                        LastModifyTime = DateTime.Now,
                        Name = "c1"
                    };
                    UserModel u = new UserModel()
                    {
                        Name = "1"
                    };

                    c1.PrintRemarkAndValueByProperty("Name");
                    u.PrintRemarkAndValueByProperty("Name");
                    Console.WriteLine("***************打印所有属性名称和值***************");
                    c.PrintNameAndValueByProperties();
                    Console.WriteLine("***************打印所有属性备注和值***************");
                    c.PrintRemarkAndValueByProperties();
                    Console.WriteLine("***************打印指定属性名称和值***************");
                    c.PrintNameAndValueByProperty("Name");
                    Console.WriteLine("***************打印指定属性备注和值***************");
                    c.PrintRemarkAndValueByProperty("Name");
                }


                var service = CreateObject.GetObject<IDbService>(ConfigManeger.GetIDbServiceConfig());
                {
                    Console.WriteLine("**********查询************");
                    var result = service.QueryById<Company>(2);
                    if (result != null)
                    {
                        Console.WriteLine("查询company Id为2成功");
                    }
                    var result1 = service.QueryAll<Company>();
                    if (result != null)
                    {
                        Console.WriteLine("查询全部company成功");
                    }
                    var result11 = service.QueryById<UserModel>(2);
                    if (result != null)
                    {
                        Console.WriteLine("查询UserModel Id为2成功");
                    }
                    var result111 = service.QueryAll<UserModel>();
                    if (result != null)
                    {
                        Console.WriteLine("查询全部UserModel成功");
                    }
                }

                {
                    Console.WriteLine("**********属性检查************");
                    UserModel user = new UserModel() { };
                    Console.WriteLine("**********属性检查,打印全部错误************");
                    var result = user.CheckValue(true);
                    if (!result.Success)
                    {
                        Console.WriteLine(result.Message);
                    }
                    Console.WriteLine("**********属性检查,打印第一个错误************");
                    var result1 = user.CheckValue();
                    if (!result1.Success)
                    {
                        Console.WriteLine(result1.Message);
                    }
                }

                {
                    Console.WriteLine("**********添加************");
                    var result = service.Insert(new Company()
                    {
                        CreateTime = DateTime.Now,
                        CreatorId = 1,
                        LastModifierId = 2,
                        LastModifyTime = null,
                        Name = "test1"
                    });
                    if (result)
                    {
                        Console.WriteLine("数据库添加company成功");
                    }
                    var result1 = service.Insert(new UserModel
                    {
                        Name = "test",
                        LastModifyTime = null,
                        Account = "1q23",
                        CompanyId = 1,
                        CompanyName = "123",
                        CreateTime = DateTime.Now,
                        CreatorId = 2,
                        Email = "1@qq.com",
                        LastLoginTime = DateTime.Now,
                        LastModifierId = 23,
                        Mobile = "asdasd",
                        Password = "123",
                        Status = 1,
                        UserType = 2

                    });
                    if (result1)
                    {
                        Console.WriteLine("数据库添加UserModel成功");
                    }
                }

                {
                    Console.WriteLine("***************删除***************");
                    var result = service.DeleteById<Company>(6);
                    if (result)
                    {
                        Console.WriteLine("删除Company成功");
                    }
                    else
                    {
                        Console.WriteLine("删除Company失败，请检查ID是否存在");
                    }
                }

                {
                    Console.WriteLine("***************修改***************");
                    var tempModel = service.QueryById<Company>(5);
                    tempModel.Name = "测试修改55";
                    var result = service.UpdateById(5, tempModel);
                    if (result)
                    {
                        Console.WriteLine("修改Company成功");
                    }
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message);
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }
    }
}
