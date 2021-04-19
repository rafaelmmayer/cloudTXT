const mongoose = require('mongoose')

const arquivosSchema = new mongoose.Schema({
    nome: String,
    conteudo: String
}, {collection: "Arquivos"})

module.exports = mongoose.model("Arquivos", arquivosSchema)
