 [[ "$(echo $LANG | cut -c 1-2)" != "pt" ]]; then
	declare -x l=( "TechJr-BackEndA" 
		       "TasksApi"
		     )
else
	declare -x l=( "TechJr-BackEndA" 
		       "TasksApi"
		      )

fi

usage(){
  cat <<EOF
${l[0]}: ${0##*/} -r TechJr-BackEndA -s TasksApi [-d TasksApi]
  
  ${l[5]}:
    -r TechJr-BackEndA  ${l[1]}
    -s TasksApi	   ${l[2]}
  ${l[6]}:
    -d TasksApi     ${l[3]}
* git-dir ${l[7]} 1.0 - Marcos Oliveira <contato@terminalroot.com.br>
** ${l[4]}
EOF

[[ ! -z "$1" ]] && exit 1

}

git-dir(){

	[[ -z $r || -z $s ]] && usage 1
	[[ ! -z $(echo "${r}" | cut -d"/" -f6) ]] && usage | tail -n 1 && exit 1
	[[ -z $(echo "${r}" | cut -d"/" -f5) ]] && usage 1
	p=$(basename "${r}")
	[[ ! -z "${d}" ]] && paramd="${d}" || paramd=
	[[ -d "/tmp/gitdir" ]] && rm -rf "/tmp/gitdir"
	mkdir "/tmp/gitdir"
	cd "/tmp/gitdir"
	git init
	git remote add -f origin $r
	git config core.sparseCheckout true
	echo "${s}" >> .git/info/sparse-checkout
	git pull origin master
	cd -
	mv "/tmp/gitdir/${s}" "./${paramd}"
	exit 0

}

while getopts 'r:s:d:' flags 2>&-; do
	case "$flags" in
		r) [[ -z "${OPTARG}" ]] && usage 1 || r=$OPTARG;;
		s) [[ -z "${OPTARG}" ]] && usage 1 || s=$OPTARG;;
		d) [[ -z "${OPTARG}" ]] && usage 1 || d=$OPTARG;;
		*) usage 1;;
	esac
done
git-dir
