# Asteroids

![Gif](https://media.giphy.com/media/XQK6MLf7LjphmSoCMv/giphy.gif)
 
[Задание](https://docs.google.com/document/d/1dw1rSdCpO5YFI-yz4sJJFzBMVc7SULHlA4q5nUgu10U/edit)

В разборе этого же тестового от [Романа Сакутина](https://www.youtube.com/watch?v=syvjmS-mflY&ab_channel=%D0%A0%D0%BE%D0%BC%D0%B0%D0%BD%D0%A1%D0%B0%D0%BA%D1%83%D1%82%D0%B8%D0%BD-GameDev) говорили, что ревьювер рекомендует рассмотреть ecs для реализации геймплея. Но одновременно с этим я был поставлен в тупик, так как сторонние фреймворки использовать нельзя. Как следствие в проекте реализована своя имплементация паттерна ECS. А так как целью этой реализации было не ускорение производительности, а более качественная реализация геймплейного кода, то моя ECS свою работу выполнила (И да простят меня за то, что это ECS на классах, а не структурах).

Игра полностью разделена по слоям:
 * Domain - внутренний слой, содержащий исключительно предметные компоненты и системы. Большинство внешних зависимостей связянных с движком вынесены в сервисы, которые инжектятся в слой
 * Presentation - визуальный слой, отвечает за показ объектов предметной области на экране - как на сцене, так и на канвасе
 * Services - содержит сервисы, которые инжектятся во внутренние слои
 * Bootstrap - запускает все внутренние слои и инжектит в них зависимости
