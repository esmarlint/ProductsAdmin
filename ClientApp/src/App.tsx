import React, { Component } from "react";
import { Route, Redirect } from "react-router";
import { Home } from "./components/Home";
import { LayoutComponent } from "./components/LayoutComponent";
import "./custom.css";
import { ProductDetail } from "./pages/ProductDetail";
import { ProductPage } from "./pages/ProductPage";

export const App = () => {
    return (
        <LayoutComponent>
            <Route exact path="/products/:id" component={ProductDetail} />
            <Route exact path="/" component={ProductPage} />
            <Redirect to="/" />
        </LayoutComponent>
    );
};
