// Live preview + price for the Custom Bracelet Builder
const form = document.getElementById("builderForm");
if (form) {
  const hex = { "Pink":"#f7a8c4","Lavender":"#cbb2ee","Mint":"#a8e6cf","Sky Blue":"#a8d8f0","Peach":"#ffcba4","White":"#ffffff","Yellow":"#fff3a3" };
  const priceEl = document.getElementById("price");
  const update = (e) => {
    let colors = [...form.querySelectorAll("input[name=colors]:checked")].map(c => c.value);
    if (colors.length > 4 && e && e.target.type === "checkbox") { e.target.checked = false; colors = colors.filter(c => c !== e.target.value); }
    const charm = document.getElementById("charm").value;
    const initial = document.getElementById("initial").value.trim().toUpperCase();
    const beads = document.getElementById("beads");
    beads.innerHTML = "";
    for (let i = 0; i < 12; i++) {
      const b = document.createElement("div");
      b.className = "bead";
      b.style.background = colors.length ? hex[colors[i % colors.length]] : "#eee";
      if (initial && i === 5) b.textContent = initial[0];
      beads.appendChild(b);
    }
    if (charm !== "None") { const c = document.createElement("div"); c.className = "bead"; c.textContent = charm[0]; c.title = charm; beads.appendChild(c); }
    const d = priceEl.dataset;
    priceEl.textContent = +d.base + (charm !== "None" ? +d.charm : 0) + (initial ? +d.initial : 0);
    document.getElementById("previewInfo").textContent = `${colors.join(", ") || "No colours yet"} | Charm: ${charm}`;
  };
  form.addEventListener("input", update);
  update();
}
