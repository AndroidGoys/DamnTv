import {
    ChangeStateResult,
    InitFunction,
    UpdateFunction,
    ViewFunction,
    IElmishComponent, 
    SetViewFunction,
    runElmishComponent
} from "../sharing/libs/elmish/index.js";

type TestModel = {
    text: string
}

type TestMessage =
    | { tag: "CHANGE_TEXT", value: string }

    

const init: InitFunction<TestModel, TestMessage> = () => {
    return {
        model: { text: "TestElmish" },
        command: null
    }
}

const update: UpdateFunction<TestModel, TestMessage> = (model, message) => {
    switch (message.tag) {
        case "CHANGE_TEXT":
            return {
                model: { text: message.value },
                command: null
            }
    }
}

const view: ViewFunction<TestModel, TestMessage, HTMLElement> = (model, dispatch) => { 
    let mainElement = document.createElement("div")

    let textSpan = document.createElement("span")
    textSpan.innerText = model.text;


    let inputText = document.createElement("input");
    inputText.addEventListener("change", () => dispatch({tag: "CHANGE_TEXT", value: inputText.textContent}))

    mainElement.appendChild(textSpan);
    mainElement.appendChild(inputText);
    return mainElement
}

const setView: SetViewFunction<HTMLElement> = (view: HTMLElement) => {
    let body = document.querySelector("body");
    body.removeChild(body.children[0]);
    body.appendChild(view);
} 

runElmishComponent(
    {
        init: init,
        update: update,
        view: view
    },
    setView
)