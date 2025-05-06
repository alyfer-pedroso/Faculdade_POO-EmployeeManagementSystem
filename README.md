# Relatório Técnico: Sistema de Gerenciamento de Funcionários

## 1. Introdução

Este relatório apresenta uma análise detalhada do Sistema de Gerenciamento de Funcionários desenvolvido em C#, destacando a arquitetura, padrões de design utilizados, estrutura de classes e decisões técnicas tomadas durante o desenvolvimento. O sistema foi projetado para atender aos requisitos especificados, implementando conceitos de Programação Orientada a Objetos como herança, polimorfismo, métodos virtuais e abstratos.

## 2. Visão Geral do Sistema

O Sistema de Gerenciamento de Funcionários é uma aplicação de console que permite o cadastro e consulta de diferentes tipos de funcionários (Gerentes, Desenvolvedores e Estagiários), calcular seus salários considerando regras específicas para cada tipo, aplicar cálculos de impostos e simular a entrega de pagamentos.

### 2.1 Objetivos Principais

- Implementar um sistema que utilize conceitos avançados de Programação Orientada a Objetos
- Permitir o cadastro de diferentes tipos de funcionários
- Calcular salários e impostos com base em regras específicas para cada tipo
- Demonstrar o uso de herança, polimorfismo, métodos virtuais e abstratos
- Implementar diferentes formas de pagamento e entrega

## 3. Arquitetura do Sistema

### 3.1 Organização de Namespaces

O projeto está organizado em namespaces que separam claramente as responsabilidades:

```
EmployeeManagementSystem
├── Factories
│   ├── IEmployeeFactory
│   ├── ManagerFactory
│   ├── DeveloperFactory
│   └── InternFactory
├── Models
│   ├── Employee
│   ├── Manager
│   ├── Developer
│   └── Intern
├── Menu
│   └── ConsoleMenu
└── Utils
    └── InputHelper
```

Esta estrutura facilita a manutenção e extensão do sistema, pois cada componente tem uma responsabilidade bem definida:

- **Models**: Contém as classes de domínio que representam os diferentes tipos de funcionários
- **Factories**: Implementa o padrão Factory Method para criar instâncias dos funcionários
- **Menu**: Gerencia a interface com o usuário através do console
- **Utils**: Fornece funcionalidades auxiliares para o sistema

### 3.2 Padrões de Design Implementados

#### 3.2.1 Factory Method

O padrão Factory Method foi implementado para encapsular a criação de objetos funcionários, permitindo que o sistema crie objetos sem conhecer suas classes concretas. Isso facilita a extensão do sistema para suportar novos tipos de funcionários no futuro.

```csharp
// Interface da fábrica
public interface IEmployeeFactory
{
    Employee CreateEmployee();
}

// Implementações concretas
public class ManagerFactory : IEmployeeFactory
{
    public Employee CreateEmployee()
    {
        Manager manager = new();
        manager.Bonus = InputHelper.ReadDecimal("Enter Bonus: ", 0);
        return manager;
    }
}

public class DeveloperFactory : IEmployeeFactory
{
    public Employee CreateEmployee()
    {
        Developer developer = new();
        developer.OvertimeHours = InputHelper.ReadInt("Enter Overtime Hours: ", 0);
        developer.OvertimeRate = InputHelper.ReadDecimal("Enter Overtime Rate: ", 0);
        return developer;
    }
}

public class InternFactory : IEmployeeFactory
{
    public Employee CreateEmployee() => new Intern();
}
```

A implementação do padrão Factory Method permite:

1. **Desacoplamento**: O código cliente não depende das classes concretas dos funcionários
2. **Extensibilidade**: Novos tipos de funcionários podem ser adicionados sem modificar o código existente
3. **Encapsulamento**: A lógica de criação dos objetos é encapsulada nas classes de fábrica

#### 3.2.2 Template Method (implícito)

Embora não implementado explicitamente, o sistema utiliza um conceito semelhante ao padrão Template Method através do método abstrato `CalculateSalary()` na classe base `Employee`, que define o "esqueleto" do algoritmo, enquanto as subclasses fornecem implementações específicas.

## 4. Hierarquia de Classes

### 4.1 Classe Base Abstrata: Employee

A classe `Employee` define a estrutura comum para todos os tipos de funcionários:

```csharp
public abstract class Employee
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Role { get; protected set; } = string.Empty;
    public decimal BaseSalary { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string DeliveryMethod { get; set; } = string.Empty;

    public abstract decimal CalculateSalary();
    public virtual decimal CalculateTaxes() => 0m;
    public virtual void DeliverPayment() => 
        Console.WriteLine($"{Name}'s paymentt will be delivered via {DeliveryMethod} using {PaymentMethod}");
    public virtual void DisplayInfo() 
    {
        Console.WriteLine($"\nName: {Name}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Role: {Role}");
        Console.WriteLine($"Base Salary: ${BaseSalary}");
        Console.WriteLine($"Payment Method: {PaymentMethod}");
        Console.WriteLine($"Delivery Method: {DeliveryMethod}");
    }
}
```

Pontos importantes:

- **Método abstrato `CalculateSalary()`**: Força as subclasses a implementarem sua própria lógica de cálculo de salário
- **Método virtual `CalculateTaxes()`**: Fornece uma implementação padrão que pode ser sobrescrita
- **Método virtual `DeliverPayment()`**: Implementação padrão que pode ser estendida
- **Propriedade protegida `Role`**: Só pode ser modificada pelas subclasses

### 4.2 Classes Derivadas

#### 4.2.1 Manager

```csharp
public class Manager : Employee
{
    public decimal Bonus { get; set; }
    const decimal _taxes = 0.275m; // 27.5%

    public Manager()
    {
        Role = "Manager";
    }

    public decimal SalaryWithBonus() => BaseSalary + Bonus;
    public override decimal CalculateTaxes() => SalaryWithBonus() * _taxes;
    public override decimal CalculateSalary()
    {
        decimal gross = SalaryWithBonus();
        return gross - CalculateTaxes();
    }
    
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Bonus: ${Bonus}");
        Console.WriteLine($"Total Salary: ${SalaryWithBonus():F2}");
        Console.WriteLine($"Taxes: ${SalaryWithBonus():F2} * ${(_taxes * 100):F2}% = {CalculateTaxes():F2}");
        Console.WriteLine($"Net Salary: ${CalculateSalary():F2}");
    }
}
```

#### 4.2.2 Developer

```csharp
public class Developer : Employee
{
    public int OvertimeHours { get; set; }
    public decimal OvertimeRate { get; set; }
    const decimal _taxes = 0.10m; // 10%

    public Developer()
    {
        Role = "Developer";
    }

    public decimal SalaryWithOvertime() => BaseSalary + (OvertimeHours * OvertimeRate);
    public override decimal CalculateTaxes() => SalaryWithOvertime() * _taxes;
    public override decimal CalculateSalary()
    {
        decimal gross = SalaryWithOvertime();
        return gross - CalculateTaxes();
    }
    
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Overtime Hours: {OvertimeHours}");
        Console.WriteLine($"Overtime Rate: {OvertimeRate}");
        Console.WriteLine($"Total Salary (${BaseSalary} + ({OvertimeHours} * ${OvertimeRate:F2})): {SalaryWithOvertime():F2}");
        Console.WriteLine($"Taxes: ${SalaryWithOvertime():F2} * ${(_taxes * 100):F2}% = {CalculateTaxes():F2}");
        Console.WriteLine($"Net Salary: ${CalculateSalary():F2}");
    }
}
```

#### 4.2.3 Intern

```csharp
public class Intern : Employee
{
    private readonly decimal _taxes = 0.10m; // 10%

    public Intern()
    {
        Role = "Intern";
    }

    public override decimal CalculateSalary() => BaseSalary;
    public override decimal CalculateTaxes() => 0.10m;
}
```

## 5. Interface com o Usuário

### 5.1 ConsoleMenu

A classe `ConsoleMenu` implementa a interface com o usuário, fornecendo métodos para exibir o menu, adicionar funcionários, listar funcionários e processar as escolhas do usuário.

Um ponto interessante é o uso de um dicionário para mapear os cargos às suas respectivas fábricas:

```csharp
private static readonly Dictionary<string, Func<IEmployeeFactory>> roleFactories = new()
{
    [validRoles[0]] = () => new ManagerFactory(),
    [validRoles[1]] = () => new DeveloperFactory(),
    [validRoles[2]] = () => new InternFactory(),
};
```

Isso permite a seleção dinâmica da fábrica correta com base na escolha do usuário:

```csharp
string roleChoice = InputHelper.ReadOption($"Enter Role: ({string.Join(", ", validRoles)}):  ", validRoles);
IEmployeeFactory factory = roleFactories.First(x => x.Key == roleChoice).Value();
var employee = factory.CreateEmployee();
```

A classe também implementa validação para evitar a duplicação de nomes:

```csharp
employee.Name = InputHelper.ReadName("Enter Name: ");

while (Array.IndexOf(employees.Select(x => x.Name).ToArray(), employee.Name) != -1)
{
    Console.WriteLine("\nEmployee with this name already exists. Please enter a different name.\n");
    employee.Name = InputHelper.ReadString("Enter Name: ");
}
```

### 5.2 InputHelper

A classe utilitária `InputHelper` fornece métodos estáticos para validar e ler dados do usuário:

```csharp
public static string ReadString(string prompt);
public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue);
public static decimal ReadDecimal(string prompt, decimal min = decimal.MinValue, decimal max = decimal.MaxValue);
public static string ReadOption(string prompt, string[] validOptions);
public static string ReadName(string prompt);
private static bool IsValidName(string input);
```

Foi adicionada uma função especializada `ReadName()` para validar nomes de funcionários, garantindo que tenham pelo menos 3 caracteres e contenham apenas letras:

```csharp
public static string ReadName(string prompt)
{
    Console.Write(prompt);
    string input = (Console.ReadLine() ?? string.Empty).Trim();

    while (!IsValidName(input))
    {
        Console.Write("Invalid name. Enter only letters (A-Z, a-z) with at least 3 characters: ");
        input = Console.ReadLine() ?? string.Empty;
    }

    return input;
}

private static bool IsValidName(string input)
{
    return input.Length >= 3 && input.All(char.IsLetter);
}
```

Esta classe melhora a robustez do sistema, garantindo que os dados fornecidos pelo usuário sejam válidos antes de serem processados.

## 6. Implementação do Cálculo de Salários e Impostos

### 6.1 Regras de Cálculo

O sistema implementa as seguintes regras para cálculo de salários e impostos:

| Tipo de Funcionário | Cálculo do Salário | Taxa de Imposto |
|---------------------|---------------------|----------------|
| Gerente             | Base + Bônus - Impostos | 27,5% |
| Desenvolvedor       | Base + (Horas Extras * Taxa) - Impostos | 10% |
| Estagiário          | Base (sem impostos) | 10% (não aplicada) |

### 6.2 Implementação

Cada classe derivada implementa sua própria lógica de cálculo:

```csharp
// Manager
public override decimal CalculateSalary()
{
    decimal gross = SalaryWithBonus();
    return gross - CalculateTaxes();
}

// Developer
public override decimal CalculateSalary()
{
    decimal gross = SalaryWithOvertime();
    return gross - CalculateTaxes();
}

// Intern
public override decimal CalculateSalary() => BaseSalary;
```

Este é um exemplo claro de polimorfismo, onde o mesmo método `CalculateSalary()` tem comportamentos diferentes dependendo do tipo de funcionário.

## 7. Decisões de Design

### 7.1 Uso de Factory Method

A decisão de utilizar o padrão Factory Method para a criação de funcionários foi baseada na necessidade de:

1. **Encapsular a lógica de criação**: Cada tipo de funcionário requer configurações específicas
2. **Facilitar a extensão do sistema**: Novos tipos de funcionários podem ser adicionados facilmente
3. **Desacoplar o código cliente das classes concretas**: O menu não precisa conhecer os detalhes de implementação de cada tipo de funcionário

### 7.2 Classes Abstratas vs. Interfaces

A escolha de uma classe abstrata `Employee` em vez de uma interface foi feita para:

1. **Fornecer implementações padrão**: Métodos como `DeliverPayment()` e `DisplayInfo()` têm comportamentos comuns que podem ser reutilizados
2. **Garantir consistência**: A classe abstrata define a estrutura comum que todas as subclasses devem seguir
3. **Combinar abstração e implementação**: Permite definir tanto métodos abstratos que devem ser implementados quanto métodos concretos que podem ser herdados ou sobrescritos

### 7.3 Métodos Virtuais vs. Abstratos

- **Métodos Abstratos**: `CalculateSalary()` foi definido como abstrato porque cada tipo de funcionário deve obrigatoriamente implementar sua própria lógica de cálculo
- **Métodos Virtuais**: `CalculateTaxes()`, `DeliverPayment()` e `DisplayInfo()` foram definidos como virtuais porque possuem implementações padrão que podem ser estendidas ou modificadas pelas subclasses

### 7.4 Constantes vs. Fields

A decisão de usar constantes para as taxas de imposto (`const decimal _taxes = 0.275m`) em vez de campos normais foi baseada na natureza fixa dessas taxas para cada tipo de funcionário. Isso garante que as taxas não possam ser alteradas acidentalmente durante a execução do programa.

### 7.5 Validação de Entrada

A implementação de métodos robustos para validação de entrada no `InputHelper` foi uma decisão importante para evitar erros e melhorar a experiência do usuário. Métodos como `ReadInt()` e `ReadDecimal()` validam a entrada do usuário e solicitam novos valores até que uma entrada válida seja fornecida.

A adição do método `ReadName()` com validação específica para nomes de funcionários aumenta a robustez do sistema, garantindo que apenas nomes válidos (com pelo menos 3 caracteres e contendo apenas letras) sejam aceitos.

## 8. Possíveis Melhorias Futuras

### 8.1 Unidades de Teste

Implementar testes unitários para garantir o funcionamento correto do sistema e facilitar futuras manutenções.

## 9. Conclusão

O Sistema de Gerenciamento de Funcionários atende com sucesso aos requisitos especificados, implementando conceitos avançados de Programação Orientada a Objetos e seguindo boas práticas de desenvolvimento. A utilização do padrão Factory Method e a estruturação clara das classes facilitam a manutenção e extensão do sistema.

O código está bem organizado, seguindo princípios de separação de responsabilidades e encapsulamento. As classes estão claramente definidas e as relações entre elas são lógicas e coerentes. O uso de conceitos como herança, polimorfismo, métodos virtuais e abstratos demonstra um bom entendimento dos princípios da Programação Orientada a Objetos.
