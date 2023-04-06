"use strict";

document.addEventListener("DOMContentLoaded", init);

function init()
{
    if (isLocalStorageSupported())
    {
        document.querySelector("form").addEventListener("submit", addListItem);
        document.querySelector("ul").addEventListener("click", removeListItem);
        renderList();
    }
    else
    {
        alert("Sorry, but your browser does not support local storage.");
        document.querySelector("form").style.visibility = "hidden";
    }
}

function addListItem(e)
{
    e.preventDefault();

    let item = document.getElementById("item").value;
    let quantity = document.getElementById("quantity").value;

    let entry = { item: item, quantity: quantity };

    let list = localStorage.getItem("list") === null ? [] : JSON.parse(localStorage.getItem("list"));

    list.push(entry);

    localStorage.setItem("list", JSON.stringify(list));

    renderList();

    document.querySelector("form").reset();
}

function removeListItem(e)
{
    e.preventDefault();

    if (e.target.tagName.toUpperCase() === "A")
    {
        let index = e.target.parentNode.getAttribute("data-index");
        
        let list = JSON.parse(localStorage.getItem("list"));
        list.splice(index, 1);
        
        localStorage.setItem("list", JSON.stringify(list));
        
        renderList();
    }
}

function renderList()
{
    let ul = document.querySelector("ul");

    ul.innerHTML = "";

    let list = localStorage.getItem("list") === null ? [] : JSON.parse(localStorage.getItem("list"));

    for (let i = 0; i < list.length; i++)
    {
        ul.innerHTML += "<li data-index=\"" + i + "\">" + list[i].item + " (" + list[i].quantity + "x) <a href=\"#\" class=\"material-icons\">delete</a></li>";
    }
}

function isLocalStorageSupported()
{
    try
    {
        return "localStorage" in window && window["localStorage"] !== null;
    }
    catch(e)
    {
        return false;
    }
}