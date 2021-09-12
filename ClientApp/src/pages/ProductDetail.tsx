import axios from "axios";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { APIResponse, Product } from "../models/api.models";
import { ColorElement } from "./ProductPage";

export const ProductDetail = () => {
    const params = useParams();
    const [product, setProduct] = useState<Product>();
    const [selectedPrice, setSelectedPrice] = useState(0);

    useEffect(() => {
        axios.get(`/api/v1/product/${params.id}`).then((response) => {
            setProduct(response.data.payload);
            setSelectedPrice(
                response.data.payload.prices.find((e) => e.isDefaultPrice).price
            );
        });
    }, []);

    return (
        <>
            {!product && <h3>Cargando...</h3>}
            {product && (
                <div>
                    <h1>{product.name}</h1>
                    <hr />
                    <h3>Descripción</h3>
                    <p>{product.description}</p>
                    <hr />
                    Precio: $<strong>{selectedPrice}</strong>
                    <hr />
                    {product.prices.map((price) => (
                        <div key={price.id}>
                            Color:
                            <ColorElement
                                colorValue={price.colorValue}
                                handleClick={() => {
                                    console.log("hola");

                                    setSelectedPrice(price.price);
                                }}
                            />
                            , Precio: <strong>${price.price}</strong>
                        </div>
                    ))}
                </div>
            )}
        </>
    );
};
