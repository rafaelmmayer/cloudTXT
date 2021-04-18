const mongoose = require('mongoose')
const Schema = mongoose.Schema;

const arquivosSchema = new Schema({
    nome: String,
    conteudo: String
}, {collection: "Arquivos"})

var Arquivos = mongoose.model("Arquivos", arquivosSchema)

module.exports = Arquivos
