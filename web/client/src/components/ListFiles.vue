<template>
    <div id="main">
        <div class="container">
            <div class="row">
                <div class="col-md">
                    <h5>Arquivos <span class="badge badge-primary">{{arquivos.length}}</span></h5>
                    <div class="card">
                        <div class="card-body arquivosList">
                            <div class="list-group">
                                <button @click="showContent(arquivo)" v-for="(arquivo, index) in arquivos" :key="index" type="button" class="list-group-item list-group-item-action">{{arquivo.nome}}</button>
                            </div>
                        </div>
                    </div>
                </div>
                <Content :arquivoSelecionado="arquivoSelecionado"/>
            </div>
            <br/>
            <div class="text-center">
                <button type="button" @click="downloadArquivo(arquivoSelecionado)" class="btn btn-primary mr-5">Baixar</button>
                <button type="button" @click="deleteArquivoSelecionado(arquivoSelecionado)" class="btn btn-primary ml-5">Deletar</button>
            </div>
        </div>
    </div>
</template>

<script>
import Content from './Content'

export default {
    components: {
        Content,
    },
    data() {
        return {
            arquivoSelecionado: {}
        }
    },
    props: {
        arquivos: Array
    },
    methods: {
        showContent(arquivo){
          this.arquivoSelecionado = arquivo
        },
        downloadArquivo(arquivo){
          const url = window.URL.createObjectURL(new Blob([arquivo.conteudo]))
          const link = document.createElement('a')
          link.href = url
          link.setAttribute('download', `${arquivo.nome}.txt`) //or any other extension
          document.body.appendChild(link)
          link.click()
        },
        async deleteArquivoSelecionado(arquivo){
            await this.$http.delete(`https://projeto-pf.herokuapp.com/api/${arquivo._id}`)
                        .then(res => console.log(`${res.body.nome} deletado`))
            location.reload()            
        }
    },
    
}
</script>

<style>
#main{
  background-color: #f1f1f1;
  padding-top: 30px;
  padding-bottom: 30px;
}
.arquivosList{
    text-align: center;
    height:500px;
    overflow-y: scroll;
}
</style>