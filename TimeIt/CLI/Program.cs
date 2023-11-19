// See https://aka.ms/new-console-template for more information
using DataAccess;

// setup
DotEnv.Load(".env");
TimeItContext context = new();

Console.WriteLine("Hello, World!");

TestObject testObject = new() { Name = "Test" };
context.TestObjects.Add(testObject);
context.SaveChanges();
