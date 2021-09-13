import axios from "axios";
import React, { useEffect, useState } from "react";
import { Color, CreateProductRequest } from "../models/api.models";
import { ColorElement } from "./ProductPage";

export const ProductCreate = () => {
    const [colors, setColors] = useState<Color[]>(null);

    const [form, setForm] = useState({
        name: "",
        description: "",
        statusId: 1,
        price: 0,
        colorId: 1,
    });

    useEffect(() => {
        axios.get("/api/v1/color").then((response) => {
            setColors(response.data.payload);
        });
    }, []);

    const handleChange = (event) => {
        setForm({
            ...form,
            [event.target.name]: event.target.value,
        });
    };

    const handleSubmit = (event) => {
        event.preventDefault();

        const request = {
            ...form,
            prices: [
                {
                    ...form,
                    statusId: 1,
                    isDefaultPrice: true,
                },
            ],
        };
        axios.post("/api/v1/product", request).then((response) => {
            console.log(response.data);
            window.alert("Producto creado");
        });
    };

    return (
        <div>
            <h1>Crear nuevo producto</h1>
            <hr />

            <div className="row">
                <div className="col-6">
                    <h4>Producto</h4>
                    <hr />
                    <form onSubmit={handleSubmit}>
                        <div className="form-group">
                            <label htmlFor="">Nombre</label>
                            <input
                                required
                                className="form-control"
                                type="text"
                                name="name"
                                value={form.name}
                                onChange={handleChange}
                            />
                        </div>
                        <div className="form-group">
                            <label htmlFor="">Descripcion</label>
                            <textarea
                                required
                                className="form-control"
                                name="description"
                                value={form.description}
                                onChange={handleChange}
                            ></textarea>
                        </div>
                        <div className="form-group">
                            <label htmlFor="">Estatus</label>
                            <select
                                required
                                className="form-control"
                                name="statusId"
                                value={form.statusId}
                                onChange={handleChange}
                            >
                                <option value="1" selected>
                                    Activo
                                </option>
                                <option value="2">Cancelado</option>
                            </select>
                        </div>
                    </form>
                </div>
                <div className="col-6">
                    <h4>Precio por defecto</h4>
                    <hr />
                    <form>
                        <div className="form-row">
                            <div className="form-group col">
                                <input
                                    required
                                    type="number"
                                    className="form-control"
                                    placeholder="Precio"
                                    name="price"
                                    value={form.price}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="form-group col">
                                <select
                                    required
                                    className="form-control"
                                    name="colorId"
                                    value={form.colorId}
                                    onChange={handleChange}
                                >
                                    {colors?.map((color) => (
                                        <option key={color.id} value={color.id} style={{ color: `#${color.value}` }}>
                                            {color.name}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                    </form>
                </div>

                <button onClick={handleSubmit} className="btn btn-primary" type="submit">
                    Crear
                </button>
            </div>
        </div>
    );
};
