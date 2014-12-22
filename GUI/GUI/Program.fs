open System.Windows.Forms
open System.Text.RegularExpressions
open NUnit.Framework
open FsUnit

// Проверка email
let isMail s =
    let login = "[a-zA-Z_]((\.)?[\w\-_]){0,30}"
    let domain = "([\w]+\.)+"
    let globaldomain = "([a-zA-Z]{2,3}|aero|asia|info|jobs|museum|name|travel|yandex)"
    let expr = new System.Text.RegularExpressions.Regex("^" + login + "@" + domain + globaldomain + "$")
    expr.IsMatch(s)

// Вспомогательные функции для графическго интерфейса
let BoolToStr b =
    if b then "Yor email is valid!" else "Sorry, but your email is unvalid!"

let check txt = BoolToStr (isMail txt)

//Графический интерфейс
let form = new Form (Text = "Email checker", Width = 305, Height = 135)
let button = new Button(Text = "Check", Top = 60, Left = 195)
let txtbox = new TextBox(Left = 20, Top = 30, Width = 250)
let label = new Label(Left = 20, Top = 10, Width = 300, Text = "Write your email and press Enter or Check button:")
txtbox.KeyPress.Add(fun arg -> 
    if (arg.KeyChar = '\r') then 
        MessageBox.Show(check (txtbox.Text), "Result") |> ignore)
button.Click.Add (fun _ -> MessageBox.Show(check txtbox.Text, "Result") |> ignore)
form.Controls.Add(button)
form.Controls.Add(txtbox)
form.Controls.Add(label)
form.Show()
Application.Run(form)

//Тесты с сайта Полозова

[<TestFixture>]
module test = 
    [<Test>]
    let ``Valid1`` () = 
        let s = "a@b.cc"
        let res = isMail s
        res |> should equal true

    [<Test>]
    let ``Valid2`` () = 
        let s = "victor.polozov@mail.ru"
        let res = isMail s
        res |> should equal true

    [<Test>]
    let ``Valid3`` () = 
        let s = "my@domain.info"
        let res = isMail s
        res |> should equal true

    [<Test>]
    let ``Valid4`` () = 
        let s = "_.1@mail.com"
        let res = isMail s
        res |> should equal true

    [<Test>]
    let ``Valid5`` () = 
        let s = "paints_department@hermitage.museum"
        let res = isMail s
        res |> should equal true

    [<Test>]
    let ``Unvalid1`` () = 
        let s = "a@b.c"
        let res = isMail s
        res |> should equal false

    [<Test>]
    let ``Unvalid2`` () = 
        let s = "a..b@mail.ru"
        let res = isMail s
        res |> should equal false

    [<Test>]
    let ``Unvalid3`` () = 
        let s = ".a@mail.ru"
        let res = isMail s
        res |> should equal false

    [<Test>]
    let ``Unvalid4`` () = 
        let s = "yo@domain.somedomain"
        let res = isMail s
        res |> should equal false

    [<Test>]
    let ``Undalid5`` () = 
        let s = "1@mail.ru"
        let res = isMail s
        res |> should equal false
