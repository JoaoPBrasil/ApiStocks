
import React, { useEffect, useState } from "react";
import axios from "axios";
import { ArrowUpRight, ArrowDownRight } from "lucide-react";
import "./App.css";

function App() {
  const [stocks, setStocks] = useState([]);

  useEffect(() => {
    axios
      .get("http://localhost:5000/stocks")
      .then((res) => setStocks(res.data))
      .catch((err) => console.error(err));
  }, []);

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4 p-4">
      {stocks.map((stock) => (
        <div key={stock.symbol} className="rounded-2xl shadow-md border p-4">
          <div className="flex items-center mb-2">
            <img src={stock.logoUrl} alt={stock.symbol} className="h-8 w-8 mr-2" />
            <div>
              <div className="font-semibold text-sm">{stock.symbol}</div>
              <div className="text-xs text-gray-500">{stock.longName}</div>
            </div>
          </div>
          <div className="text-lg font-bold">R$ {stock.regularMarketPrice.toFixed(2)}</div>
          <div
            className={\`inline-flex items-center text-sm font-medium mt-2 px-2 py-1 rounded \${stock.regularMarketChangePercent >= 0 ? "bg-green-100 text-green-700" : "bg-red-100 text-red-700"}\`}
          >
            {stock.regularMarketChangePercent >= 0 ? (
              <ArrowUpRight className="w-4 h-4 mr-1" />
            ) : (
              <ArrowDownRight className="w-4 h-4 mr-1" />
            )}
            {stock.regularMarketChangePercent.toFixed(2)}%
          </div>
        </div>
      ))}
    </div>
  );
}

export default App;
