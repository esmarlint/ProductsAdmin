import React, { Component } from "react";
import { Route, Redirect } from "react-router";
import { Home } from "./components/Home";
import { LayoutComponent } from "./components/LayoutComponent";
import "./custom.css";
import { ProductPage } from "./pages/ProductPage";

export const App = () => {
    return (
        <LayoutComponent>
            <Route path="/" component={ProductPage} />
            <Redirect to="products" />
        </LayoutComponent>
    );
};
